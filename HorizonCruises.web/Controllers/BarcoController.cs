using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace HorizonCruises.web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;

        public BarcoController(IServiceBarco serviceBarco)
        {
            _serviceBarco = serviceBarco;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection);
        }

        public async Task<ActionResult> IndexAdmin(int? page)
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: BarcoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var @object = await _serviceBarco.FindByIdAsync(id);
            return View(@object);
        }
    }
}
