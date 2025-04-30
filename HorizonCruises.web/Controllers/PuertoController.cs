using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PuertoIndex()
        {
            var collection = await _servicePurto.ListAsync();
            return View(collection);
        }

        public async Task<IActionResult> GetPuertoByName(string filtro)
        {
            var collection = await _servicePurto.FindByNameAsync(filtro);
            return Json(collection);
        }


    }
}
