using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
