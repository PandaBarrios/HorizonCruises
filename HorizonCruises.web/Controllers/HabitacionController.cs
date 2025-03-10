using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace HorizonCruises.web.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly IServiceHabitacion _serviceHabitacion;

        public HabitacionController(IServiceHabitacion serviceHabitacion)
        {
            _serviceHabitacion = serviceHabitacion;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceHabitacion.ListAsync();
            return View(collection);
        }

        public async Task<ActionResult> IndexAdmin(int? page)
        {
            var collection = await _serviceHabitacion.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: HabitacionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var @object = await _serviceHabitacion.FindByIdAsync(id);
            return View(@object);
        }
    }
}
