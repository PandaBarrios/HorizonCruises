using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HorizonCruises.web.Controllers
{
    [Authorize]
    public class TarjetaController : Controller
    {
        private readonly IServiceTarjeta _serviceTarjeta;
        private readonly ILogger<TarjetaController> _logger;

        public TarjetaController(IServiceTarjeta serviceTarjeta, ILogger<TarjetaController> logger)
        {
            _serviceTarjeta = serviceTarjeta;
            _logger = logger;
        }


        // Acción Index para mostrar solo las tarjetas del usuario logueado
        public async Task<IActionResult> Index()
        {
            // Obtener el IdUsuario desde el claim
            var userIdClaim = User.FindFirst("IdUsuario")?.Value;

            if (userIdClaim == null)
            {
                // Si no se encuentra el Claim, redirigir al login
                _logger.LogWarning("No se pudo obtener el ID del usuario logueado.");
                return RedirectToAction("Login", "Index");
            }

            // Convertir el userId a entero
            int userId = int.Parse(userIdClaim);

            // Obtener las tarjetas del usuario logueado
            var tarjetas = await _serviceTarjeta.GetTarjetasByUsuarioIdAsync(userId);

            // Pasar las tarjetas filtradas a la vista
            return View(tarjetas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var tarjeta = await _serviceTarjeta.FindByIdAsync(id);
            return View(tarjeta);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TarjetaDTO tarjeta)
        {
            // Verificar que el usuario está logueado antes de intentar obtener el IdUsuario
            var userIdClaim = User.FindFirst("IdUsuario")?.Value;

            if (userIdClaim == null)
            {
                // Si no se encuentra el Claim, mostrar un mensaje de error y redirigir.
                _logger.LogWarning("No se pudo obtener el ID del usuario logueado.");
                return RedirectToAction("Login", "Index");
            }

            // Convertir el userId a entero si es necesario
            int userId = int.Parse(userIdClaim);

            // Ahora que tienes el IdUsuario, asignalo al modelo de la tarjeta
            tarjeta.IdUsuario = userId;

            try
            {
                // Llamar al servicio y esperar que se complete
                await _serviceTarjeta.AddAsync(tarjeta);

                TempData["Message"] = "Tarjeta creada correctamente";
                return RedirectToAction("Index");  // Redirige a la vista deseada
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al crear la tarjeta: {ex.Message}");
                TempData["Message"] = "Hubo un error al crear la tarjeta";
                return View(tarjeta);  // Regresa al formulario para corregir
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tarjeta = await _serviceTarjeta.FindByIdAsync(id);
            return View(tarjeta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TarjetaDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _serviceTarjeta.UpdateAsync(id, dto);
            return RedirectToAction("Index");
        }
    }
}

