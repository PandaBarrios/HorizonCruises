using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
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
    }
}
