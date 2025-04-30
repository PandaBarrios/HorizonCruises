using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using X.PagedList.Extensions;

namespace HorizonCruises.web.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly IServiceHabitacion _serviceHabitacion;

        public HabitacionController(IServiceHabitacion serviceHabitacion)
        {
            _serviceHabitacion = serviceHabitacion;
        }

        [HttpGet]
        // GET: HabitacionController
        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<ActionResult> Index()
        {
            var collection = await _serviceHabitacion.ListAsync();
            return View(collection);
        }
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> IndexAdmin()
        {
            var collection = await _serviceHabitacion.ListAsync();
            return View(collection);
        }

        // GET: HabitacionController/Details/5
        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<ActionResult> Details(int id)
        {
            var @object = await _serviceHabitacion.FindByIdAsync(id);
            return View(@object);
        }

        // GET: HabitacionController/Create
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(HabitacionDTO dto)
        {
            // Validación del modelo
            if (!ModelState.IsValid)
            {
                string errors = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            // Validar que el nombre de la habitación sea único
            var existeHabitacion = await _serviceHabitacion.ListAsync();
            if (existeHabitacion.Any(h => h.Nombre == dto.Nombre))
            {
                ModelState.AddModelError("Nombre", "Ya existe una habitación con este nombre.");
                return View(dto);
            }

            // 📌 Depuración: Imprime los datos antes de guardarlos
            Console.WriteLine($"Id: {dto.Id}, Nombre: {dto.Nombre}, Descripción: {dto.Descripcion}, " +
                   $"CantidadMinimaHuespedes: {dto.CantidadMinimaHuespedes}, " +
                   $"CantidadMaximaHuespedes: {dto.CantidadMaximaHuespedes}, " +
                   $"Tamaño: {dto.Tamano}, Tipo: {dto.Tipo}");

            // Intentar guardar la habitación
            int result = await _serviceHabitacion.AddAsync(dto);

            if (result == -1)  // Indica que hubo un error
            {
                ModelState.AddModelError("", "No se pudo crear la habitación. Verifica los datos.");
                return View(dto); // Vuelve a mostrar la vista con el error
            }

            return RedirectToAction("IndexAdmin");
        }

        // GET: HabitacionController/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            var habitacion = await _serviceHabitacion.FindByIdAsync(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: HabitacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, HabitacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
                ViewBag.ErrorMessage = errors;
                return View(dto);
            }

            var habitacionExistente = await _serviceHabitacion.FindByIdAsync(id);
            if (habitacionExistente == null)
            {
                return NotFound();
            }

            // Validar que el nuevo nombre no pertenezca a otra habitación diferente
            var existeOtraHabitacion = await _serviceHabitacion.ListAsync();
            if (existeOtraHabitacion.Any(h => h.Nombre == dto.Nombre && h.Id != id))
            {
                ModelState.AddModelError("Nombre", "Ya existe otra habitación con este nombre.");
                return View(dto);
            }

            await _serviceHabitacion.UpdateAsync(id, dto);

            return RedirectToAction("Index");
        }
    }
}
