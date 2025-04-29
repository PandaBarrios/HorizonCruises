using HorizonCruises.Application.DTOs;
using HorizonCruises.Application;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using HorizonCruises.Infraestructure.Models;

namespace HorizonCruises.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceCliente _serviceUsuario;
        private readonly IServiceRol _serviceRol;
        private readonly HttpClient _httpClient;

        public UsuarioController(IServiceCliente serviceUsuario, IServiceRol serviceRol, HttpClient httpClient)
        {
            _serviceUsuario = serviceUsuario;
            _serviceRol = serviceRol;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        [HttpGet]
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

        // Método para obtener la lista de países desde la API externa
        private async Task<List<string>> ObtenerListaPaisesDesdeAPI()
        {
            var response = await _httpClient.GetStringAsync("https://restcountries.com/v3.1/all");
            var countries = JsonConvert.DeserializeObject<List<Pais>>(response);

            return countries.Select(c => c.name.common).OrderBy(p => p).ToList();
        }

        // GET: UsuarioController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Paises = await ObtenerListaPaisesDesdeAPI();
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _serviceUsuario.AddAsync(dto);

            TempData["SuccessMessage"] = "Cliente creado exitosamente."; // Guardamos el mensaje en TempData
            return RedirectToAction("Index", "Login"); // Redirigimos a Login
        }

        public async Task<IActionResult> Details(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteDTO dto)
        {
            await _serviceUsuario.UpdateAsync(id, dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _serviceUsuario.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
