using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace HorizonCruises.web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceBarco _serviceBarco;
        private readonly IServiseItinerario _serviceItinerario;
        private readonly IServicePuerto _servicePuerto;

        private readonly ILogger<ServiceCrucero> _logger;

        public CruceroController(IServiceCrucero serviceCrucero, IServiceBarco serviceBarco, IServiseItinerario serviceItinerario, IServicePuerto servicePuerto, ILogger<ServiceCrucero> logger)
        {
            _serviceCrucero = serviceCrucero;
            _serviceBarco = serviceBarco;
            _serviceItinerario = serviceItinerario;
            _servicePuerto = servicePuerto;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CruceroIndex()
        {
            var collection = await _serviceCrucero.ListAsync();
            return View(collection);
        }

        public async Task<ActionResult> CruceroDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("CruceroIndex");
                }
                var @object = await _serviceCrucero.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Crucero no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerImagen(int id)
        {
            var crucero = await _serviceCrucero.FindByIdAsync(id);

            if (crucero == null || crucero.Imagen == null)
            {
                return NotFound();
            }

            return File(crucero.Imagen, "image/jpeg");
        }

        //Mantenimiento Crucero
        public async Task<ActionResult> IndexAdmin()
        {
            var collection = await _serviceCrucero.ListAsync();
            return View(collection);
        }

        // GET: CruceroController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListBarco = await _serviceBarco.ListAsync();
            ViewBag.ListPuerto = await _servicePuerto.ListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CruceroDTO cruceroDTO, IFormFile ImageFile, string itinerarioJson)
        {

            try
            {
                // Procesar la imagen si se ha subido
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ImageFile.CopyToAsync(memoryStream);
                        cruceroDTO.Imagen = memoryStream.ToArray();
                    }
                }

                // Deserializar el itinerario
                var itinerario = JsonSerializer.Deserialize<List<ItinerarioDTO>>(itinerarioJson);
                if (itinerario == null || !itinerario.Any())
                {
                    ModelState.AddModelError("Itinerario", "El itinerario no puede estar vacío.");
                    ViewBag.ListBarco = await _serviceBarco.ListAsync();
                    ViewBag.ListPuerto = await _servicePuerto.ListAsync();
                    return View(cruceroDTO);
                }

                // Establecer la cantidad de días
                cruceroDTO.CantidadDias = itinerario.Count;

                // Guardar crucero
                var cruceroCreado = await _serviceCrucero.CreateAsync(cruceroDTO);

                // Guardar el itinerario
                foreach (var item in itinerario)
                {
                    item.IdCrucero = cruceroCreado.Id;
                    await _serviceItinerario.CreateAsync(item);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el crucero y su itinerario.");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el crucero.");
                ViewBag.ListBarco = await _serviceBarco.ListAsync();
                ViewBag.ListPuerto = await _servicePuerto.ListAsync();
                return View(cruceroDTO);
            }
        }

        public async Task<ActionResult> DetailsAdmin(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                var @object = await _serviceCrucero.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Crucero no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
