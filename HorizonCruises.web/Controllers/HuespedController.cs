using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class HuespedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
