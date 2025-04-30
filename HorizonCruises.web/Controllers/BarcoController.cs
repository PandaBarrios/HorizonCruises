using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using X.PagedList.Extensions;

namespace HorizonCruises.web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;
        private readonly IServiceHabitacion _serviceHabitacion;
        private readonly IServiceBarcoHabitaciones _serviceBarcoHabitacion;

        public BarcoController(IServiceBarco serviceBarco, IServiceHabitacion serviceHabitacion, IServiceBarcoHabitaciones serviceBarcoHabitacion)
        {
            _serviceBarco = serviceBarco;
            _serviceHabitacion = serviceHabitacion;
            _serviceBarcoHabitacion = serviceBarcoHabitacion;
        }

        [HttpGet]
        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> IndexAdmin(int? page)
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<IActionResult> Details(int id)
        {
            var barco = await _serviceBarco.FindByIdAsync(id);
            return View(barco);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create()
        {
            var habitaciones = await _serviceHabitacion.ListAsync();
            ViewBag.Habitaciones = new SelectList(habitaciones, "Id", "Nombre");
            return View(new BarcoDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(BarcoDTO dto)
        {
            var habitaciones = await _serviceHabitacion.ListAsync();
            ViewBag.Habitaciones = new SelectList(habitaciones, "Id", "Nombre");

            if (dto == null || !ModelState.IsValid)
            {
                return View(dto);
            }

            if (dto.BarcoHabitaciones != null)
            {
                var habitacionesDuplicadas = dto.BarcoHabitaciones
                    .GroupBy(h => h.IdHabitacion)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (habitacionesDuplicadas.Any())
                {
                    ModelState.AddModelError("BarcoHabitaciones", "No puedes agregar la misma habitación más de una vez.");
                    return View(dto);
                }
            }

            var result = await _serviceBarco.AddAsync(dto);
            if (!result)
            {
                ModelState.AddModelError("", "No se pudo crear el barco.");
                return View(dto);
            }

            return RedirectToAction(nameof(IndexAdmin));
        }

        // GET: BarcoController/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            var barco = await _serviceBarco.FindByIdAsync(id);

            if (barco == null)
            {
                return NotFound();
            }

            // PASO 1: Cargar todas las habitaciones disponibles
            ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();

            // PASO 2: Obtener habitaciones ASIGNADAS (habitaciones actuales del barco)
            var habitacionesAsignadas = await _serviceBarcoHabitacion.GetHabitacionesByBarcoAsync(id); // ← Aquí llamas al método correcto que ya tienes&#8203;:contentReference[oaicite:3]{index=3}

            ViewBag.HabitacionesAsignadas = habitacionesAsignadas; // ← Pasarlas al Razor

            return View(barco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, BarcoDTO barcoDTO, string habitacionesJson)
        {
            if (id != barcoDTO.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();
                return View(barcoDTO);
            }

            var barco = await _serviceBarco.FindByIdAsync(id);
            if (barco == null)
                return NotFound();

            // ✅ 1. Eliminar relaciones antiguas de habitaciones
            await _serviceBarcoHabitacion.RemoveByBarcoIdAsync(barco.Id);

            // ✅ 2. Agregar nuevas habitaciones desde JSON
            var habitaciones = JsonSerializer.Deserialize<List<BarcoHabitacionesDTO>>(habitacionesJson);
            if (habitaciones != null && habitaciones.Any())
            {
                barco.BarcoHabitaciones = new List<BarcoHabitaciones>();

                foreach (var habitacion in habitaciones)
                {
                    barco.BarcoHabitaciones.Add(new BarcoHabitaciones
                    {
                        IdBarco = barco.Id,
                        IdHabitacion = habitacion.IdHabitacion,
                        TotalHabitacionesDisponibles = habitacion.TotalHabitacionesDisponibles
                    });
                }
            }
            else
            {
                barco.BarcoHabitaciones = new List<BarcoHabitaciones>(); // ← Asegura que no quede nulo
            }

            // ✅ 3. Actualizar datos del barco
            barco.Nombre = barcoDTO.Nombre;
            barco.Descripcion = barcoDTO.Descripcion;
            barco.CapacidadHuespedes = barcoDTO.CapacidadHuespedes;

            // ✅ 4. Ejecutar la actualización
            var result = await _serviceBarco.UpdateAsync(barco);
            if (!result)
            {
                ModelState.AddModelError("", "No se pudo actualizar el barco.");
                ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();
                return View(barcoDTO);
            }

            // ✅ 5. Redirigir
            return RedirectToAction(nameof(IndexAdmin));
        }
    }
}
