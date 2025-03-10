using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class PuertoController : Controller
    {
        private readonly IServicePuerto _servicePurto;

        public PuertoController(IServicePuerto serviceCrucero)
        {
            _servicePurto = serviceCrucero;
        }

        public async Task<IActionResult> PuertoIndex()
        {
            var collection = await _servicePurto.ListAsync();
            return View(collection);
        }
    }
}
