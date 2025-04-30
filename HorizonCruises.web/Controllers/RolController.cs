using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class RolController : Controller
    {
        private readonly IServiceRol _serviceRol;

        public RolController(IServiceRol serviceRol)
        {
            _serviceRol = serviceRol;
        }
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceRol.ListAsync();
            return View(collection);
        }
    }
}
