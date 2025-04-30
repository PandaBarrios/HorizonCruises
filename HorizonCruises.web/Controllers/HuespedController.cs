using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class HuespedController : Controller
    {
        private readonly IServiceHuesped _serviceHuesped;
        private readonly IServiceUsuarioHuesped _serviceUsuarioHuesped;
        private readonly ILogger<ServiceHuesped> _logger;

        public HuespedController(IServiceHuesped serviceHuesped, IServiceUsuarioHuesped serviceUsuarioHuesped, ILogger<ServiceHuesped> logger)
        {
            _serviceHuesped = serviceHuesped;
            _serviceUsuarioHuesped = serviceUsuarioHuesped;
            _logger = logger;
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> HuspedIndex()
        {
            if (!int.TryParse(User.FindFirst("IdUsuario")?.Value, out int idUsuario))
                return Unauthorized();

            var collection = await _serviceUsuarioHuesped.GetHuespedByUsuarioAsync(idUsuario);
            return View(collection);
        }

        [Authorize(Roles = "Cliente")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Create(ViewModelHuesped model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!int.TryParse(User.FindFirst("IdUsuario")?.Value, out int idUsuario))
                return Unauthorized();

            try
            {
                // Crear el nuevo huésped
                var nuevoHuesped = new HuespedDTO
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Correo = model.Correo,
                    FechaNacimiento = model.FechaNacimiento,
                    Pasaporte = model.Pasaporte,
                    Genero = model.Genero.Value
                };

                var huespedCreado = await _serviceHuesped.AddAsync(nuevoHuesped);

                // Enlazar con el usuario actual
                await _serviceUsuarioHuesped.AddAsync(idUsuario, huespedCreado.Id);

                return RedirectToAction("HuspedIndex");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear huésped");
                ModelState.AddModelError("", "Ocurrió un error al guardar el huésped.");
                return View(model);
            }
        }

    }
}
