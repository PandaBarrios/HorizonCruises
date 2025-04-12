using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace HorizonCruises.web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReservaController : Controller
    {
        private readonly IServiceReserva _serviceReserva;

        public ReservaController(IServiceReserva serviceReserva)
        {
            _serviceReserva = serviceReserva;
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

    }


}
