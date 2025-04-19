using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Rotativa.AspNetCore;
using System.Runtime.CompilerServices;

namespace HorizonCruises.web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReservaController : Controller
    {
        private readonly IServiceReserva _serviceReserva;
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceCliente _serviceCliente;
        private readonly IServiceBarcoHabitaciones _serviceBarcoHabitaciones;

        private readonly ILogger<ServiceReserva> _logger;

        public ReservaController(IServiceReserva serviceReserva, IServiceCrucero serviceCrucero, IServiceCliente serviceCliente, IServiceBarcoHabitaciones serviceBarcoHabitaciones, ILogger<ServiceReserva> logger)
        {
            _serviceReserva = serviceReserva;
            _serviceCrucero = serviceCrucero;
            _serviceCliente = serviceCliente;
            _serviceBarcoHabitaciones = serviceBarcoHabitaciones;
            _logger = logger;
        }

        public async Task<IActionResult> IndexReserva()
        {
            var collection = await _serviceReserva.ListAsync();
            return View(collection);

        }
        public async Task<IActionResult> DetailsReserva(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexReserva");
                }
                var @object = await _serviceReserva.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Reserva no existente");

                }
                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Acción que genera un archivo PDF basado en una vista Razor
        public async Task<IActionResult> GenerarFacturaPDF(int? id)
        {
            // Busca la reserva correspondiente usando su ID
            var reserva = await _serviceReserva.FindByIdAsync(id.Value);

            // Si no existe la reserva, redirige al listado
            if (reserva == null)
            {
                return RedirectToAction("IndexReserva");
            }

            // Genera un PDF usando la vista "DetailsReserva" y pasando el objeto reserva como modelo
            return new ViewAsPdf("DetailsReserva", reserva)
            {
                // Nombre del archivo PDF que se descargará
                FileName = $"FacturaReserva_{id}.pdf",

                // Tamaño del papel: A4 (clásico de impresora)
                PageSize = Rotativa.AspNetCore.Options.Size.A4,

                // Orientación de la página: vertical (Portrait)
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }

        public async Task<IActionResult> Crear(int? id)
        {
            
            if (!int.TryParse(User.FindFirst("IdUsuario")?.Value, out int idUsuario))
                return Unauthorized();

            var crucero = await _serviceCrucero.FindByIdAsync(id!.Value);
            if (crucero == null) return NotFound();

            var usuarioDTO = await _serviceCliente.FindByIdAsync(idUsuario);

            var habitacionesDTO = await _serviceBarcoHabitaciones.GetHabitacionesByBarcoAsync(crucero.IdBarco);

            var reserva = new ReservaDTO
            {
                FechaReserva = DateOnly.FromDateTime(DateTime.Now),
                IdCrucero = crucero.Id,
                IdCruceroNavigation = crucero,
                IdUsuario = usuarioDTO.Id,
                IdUsuarioNavigation = usuarioDTO,
                
            };

            var viewModel = new ViewModelReserva
            {
                Reserva = reserva,
                Habitaciones = habitacionesDTO.ToList(),
            };

            return View(viewModel);
        }




    }


}
