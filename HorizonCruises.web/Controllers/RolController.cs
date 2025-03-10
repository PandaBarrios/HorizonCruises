using HorizonCruises.Application.Services.Interfaces;
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
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceRol.ListAsync();
            return View(collection);
        }
    }
}
