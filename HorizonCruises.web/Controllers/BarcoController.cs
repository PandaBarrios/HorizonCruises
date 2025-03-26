using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace HorizonCruises.web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;
        private readonly IServiceHabitacion _serviceHabitacion;

        public BarcoController(IServiceBarco barcoService, IServiceHabitacion habitacionService)
        {
            _serviceBarco = barcoService;
            _serviceHabitacion = habitacionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection);
        }

        public async Task<ActionResult> IndexAdmin(int? page)
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: BarcoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var @object = await _serviceBarco.FindByIdAsync(id);
            return View(@object);
        }

        // GET: BarcoController/Create
        public async Task<IActionResult> Create()
        {
            // Obtener las habitaciones desde la base de datos (en este ejemplo se asume que "Nombre" es el nombre de la habitación)
            var habitaciones = await _serviceHabitacion.ListAsync(); // Aquí obtienes una lista de habitaciones
            ViewBag.Habitaciones = new SelectList(habitaciones, "Id", "Nombre"); // Pasamos Id y Nombre de la habitación

            return View();
        }

        // Crear un barco
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarcoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Validación del modelo
            if (!ModelState.IsValid)
            {
                ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();
                return View(dto);
            }

            // Verificar si hay habitaciones duplicadas
            if (dto.BarcoHabitaciones != null && dto.BarcoHabitaciones.Any())
            {
                var habitacionesDuplicadas = dto.BarcoHabitaciones
                    .GroupBy(h => h.IdHabitacion)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (habitacionesDuplicadas.Any())
                {
                    ModelState.AddModelError("BarcoHabitaciones", "No puedes agregar la misma habitación más de una vez.");
                    ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();
                    return View(dto);
                }
            }

            // Guardar el barco si no hay errores
            var result = await _serviceBarco.AddAsync(dto);
            if (!result)
            {
                return BadRequest("No se pudo crear el barco.");
            }

            return RedirectToAction("Index");
        }

        // GET: BarcoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var barco = await _serviceBarco.FindByIdAsync(id);

            if (barco == null)
            {
                return NotFound();
            }

            // Pasar la lista de habitaciones del barco a la vista
            ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();
            // Extraer solo los IdHabitacion asociados al barco
            ViewBag.HabitacionesSeleccionadas = barco.BarcoHabitaciones.Select(bh => bh.IdHabitacion).ToList();

            return View(barco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BarcoDTO barcoDTO)
        {
            if (id != barcoDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var barco = await _serviceBarco.FindByIdAsync(id);

                if (barco == null)
                {
                    return NotFound();
                }

                // **1. Limpiar las habitaciones del barco**
                barco.BarcoHabitaciones.Clear();

                // **2. Agregar las habitaciones seleccionadas**
                if (barcoDTO.BarcoHabitaciones != null)
                {
                    foreach (var habitacionDTO in barcoDTO.BarcoHabitaciones)
                    {
                        barco.BarcoHabitaciones.Add(new BarcoHabitaciones
                        {
                            IdHabitacion = habitacionDTO.IdHabitacion,
                            TotalHabitacionesDisponibles = habitacionDTO.TotalHabitacionesDisponibles
                        });
                    }
                }

                // **3. Actualizar la información del barco**
                barco.Nombre = barcoDTO.Nombre;
                barco.Descripcion = barcoDTO.Descripcion;
                barco.CapacidadHuespedes = barcoDTO.CapacidadHuespedes;

                // **4. Guardar cambios usando el servicio**
                var result = await _serviceBarco.UpdateAsync(barco);

                if (!result)
                {
                    return BadRequest("No se pudo actualizar el barco.");
                }

                return RedirectToAction(nameof(Index));
            }

            // Recargar lista de habitaciones si hay error
            ViewBag.Habitaciones = await _serviceHabitacion.ListAsync();

            return View(barcoDTO);
        }
    }
}
