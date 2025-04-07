using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceCliente _serviceUsuario;
        private readonly IServiceRol _serviceRol;

        public UsuarioController(IServiceCliente serviceUsuario, IServiceRol serviceRol)
        {
            _serviceUsuario = serviceUsuario;
            _serviceRol = serviceRol;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        // GET:  
        public async Task<IActionResult> Login(string id, string password)
        {
            var @object = await _serviceUsuario.LoginAsync(id, password);
            if (@object == null)
            {
                ViewBag.Message = "Error en Login o Password";
                return View("Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        // GET: UsuarioController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListRoles = await _serviceRol.ListAsync();
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteDTO dto)
        {

            await _serviceUsuario.AddAsync(dto);
            return RedirectToAction("Login");

        }


        // GET: UsuarioController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ClienteDTO dto)
        {
            await _serviceUsuario.UpdateAsync(id, dto);
            return RedirectToAction("Index");
        }

        // GET: UsuarioController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            await _serviceUsuario.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
