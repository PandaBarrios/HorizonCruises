using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Text.Json;

namespace HorizonCruises.web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceBarco _serviceBarco;
        private readonly IServiseItinerario _serviceItinerario;
        private readonly IServicePuerto _servicePuerto;
        private readonly IServiceFechaCrucero _serviceFechaCrucero;
        private readonly IServiceBarcoHabitaciones _serviceBarcoHabitaciones;

        private readonly IServiceReserva _serviceReserva;
        private readonly ILogger<ServiceCrucero> _logger;

        public CruceroController(IServiceCrucero serviceCrucero, IServiceBarco serviceBarco, IServiseItinerario serviceItinerario, IServicePuerto servicePuerto, IServiceFechaCrucero serviceFechaCrucero, IServiceBarcoHabitaciones serviceBarcoHabitaciones, IServiceReserva serviceReserva, ILogger<ServiceCrucero> logger)
        {
            _serviceCrucero = serviceCrucero;
            _serviceBarco = serviceBarco;
            _serviceItinerario = serviceItinerario;
            _servicePuerto = servicePuerto;
            _serviceFechaCrucero = serviceFechaCrucero;
            _serviceBarcoHabitaciones = serviceBarcoHabitaciones;
            _serviceReserva = serviceReserva;
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
            ViewBag.ListFechasCrucero = await _serviceFechaCrucero.ListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CruceroDTO cruceroDTO,
                                         IFormFile ImageFile,
                                         string itinerarioJson,
                                         string FechaInicio,
                                         string FechaLimitePago,
                                         string preciosHabitacionesJson)
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

                // Validar que no existan puertos repetidos
                var puertosDuplicados = itinerario.GroupBy(i => i.IdPuerto)
                                                  .Where(g => g.Count() > 1)
                                                  .Select(g => g.Key)
                                                  .ToList();

                if (puertosDuplicados.Any())
                {
                    ModelState.AddModelError(string.Empty, "No puede haber puertos repetidos en el itinerario.");
                    ViewBag.ListBarco = await _serviceBarco.ListAsync();
                    ViewBag.ListPuerto = await _servicePuerto.ListAsync();
                    return View(cruceroDTO);
                }


                // Deserializar los precios habitaciones
                var preciosHabitaciones = JsonSerializer.Deserialize<List<PrecioHabitacionDTO>>(preciosHabitacionesJson);

                if (preciosHabitaciones == null || !preciosHabitaciones.Any())
                {
                    ModelState.AddModelError("Precios", "Debe agregar precios para las habitaciones.");
                    ViewBag.ListBarco = await _serviceBarco.ListAsync();
                    ViewBag.ListPuerto = await _servicePuerto.ListAsync();
                    return View(cruceroDTO);
                }

                // Validar y convertir fechas
                if (!DateOnly.TryParseExact(FechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly fechaInicioConvertida) ||
                    !DateOnly.TryParseExact(FechaLimitePago, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly fechaLimiteConvertida))
                {
                    ModelState.AddModelError("Fecha", "Formato de fecha inválido.");
                    ViewBag.ListBarco = await _serviceBarco.ListAsync();
                    ViewBag.ListPuerto = await _servicePuerto.ListAsync();
                    return View(cruceroDTO);
                }

                // Establecer la cantidad de días
                cruceroDTO.CantidadDias = itinerario.Count;

                // Guardar el Crucero
                var cruceroCreado = await _serviceCrucero.CreateAsync(cruceroDTO);

                // Guardar el Itinerario
                foreach (var item in itinerario)
                {
                    item.IdCrucero = cruceroCreado.Id;
                    await _serviceItinerario.CreateAsync(item);
                }

                // Guardar la Fecha Crucero
                var fechaCrucero = new FechaCruceroDTO
                {
                    IdCrucero = cruceroCreado.Id,
                    FechaInicio = fechaInicioConvertida,
                    FechaLimitePago = fechaLimiteConvertida
                };

                var fechaCruceroCreada = await _serviceFechaCrucero.CreateAsync(fechaCrucero);

                // Guardar precios de habitaciones
                foreach (var precio in preciosHabitaciones)
                {
                    var nuevoPrecio = new PrecioHabitacionDTO
                    {
                        IdCruceroFecha = fechaCruceroCreada.Id,
                        IdHabitacion = precio.IdHabitacion,
                        PrecioHabitacion1 = precio.PrecioHabitacion1,
                        FechaLimitePrecio = precio.FechaLimitePrecio
                    };

                    await _serviceFechaCrucero.CreatePrecioHabitacionAsync(nuevoPrecio);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el crucero y sus componentes.");
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

        [HttpGet]
        public async Task<IActionResult> GetHabitacionesPorBarco(int idBarco)
        {
            var habitaciones = await _serviceBarcoHabitaciones.HabitacionesPorBarcoAsync(idBarco);
            return Json(habitaciones);
        }

    }
}
