using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.web.ViewModels;
using HorizonCruises.web.Util;

namespace HorizonCruises.web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceCliente _serviceUsuario;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IServiceCliente serviceUsuario, ILogger<LoginController> logger)
        {
            _serviceUsuario = serviceUsuario;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ViewModelLogin viewModelLogin)
        {
            //Revisar que haga lo que nosotros necesitamos
            if (!ModelState.IsValid)
            {
                string errors = string.Join(" ; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));

                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", $"Error de Acceso {errors}", SweetAlertMessageType.error);

                _logger.LogInformation($"Error en login de {viewModelLogin}, Errores --> {errors}");
                return View("Index");
            }

            //Verificar sí el usuario existe
            var usuarioDTO = await _serviceUsuario.LoginAsync(viewModelLogin.CorreoElectronico, viewModelLogin.Contrasena);
            if (usuarioDTO == null)
            {
                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Error de Acceso", SweetAlertMessageType.error);
                _logger.LogInformation($"Error en login de {viewModelLogin.CorreoElectronico}, Error --> Error en acceso");
                return View("Index");
            }

            //Claim almacena información del usuario como nombre, rol y otros. Esto se guardará en una cookie
            List<Claim> claims = new List<Claim>() {
                 new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                 new Claim(ClaimTypes.Role, usuarioDTO.IdRolNavigation.Nombre!),
                 new Claim("IdUsuario", usuarioDTO.Id.ToString()) // ← Agrega la ID del usuario
            };


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            _logger.LogInformation($"Conexion correcta de {viewModelLogin.CorreoElectronico}");
            TempData["Message"] = Util.SweetAlertHelper.Mensaje("Login", "Usuario identificado", SweetAlertMessageType.success);

            return RedirectToAction("Index", "Home");
        }

        /* Solo la usuario conectada puede cerrar sesión */
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            _logger.LogInformation($"Desconexion correcta de {User!.Identity!.Name}");
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Login");
        }
        public IActionResult Forbidden() // Cuando el acceso sea denegado muestra Forboden
        {
            return View();
        }
    }
}
