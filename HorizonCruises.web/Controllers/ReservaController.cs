// Archivo completo corregido: ReservaController.cs
using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Models;
using HorizonCruises.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System.Globalization;
using System.Text.Json;

namespace HorizonCruises.web.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IServiceReserva _serviceReserva;
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceCliente _serviceCliente;
        private readonly IServiceBarcoHabitaciones _serviceBarcoHabitaciones;
        private readonly IServiceUsuarioHuesped _serviceUsuarioHuesped;
        private readonly IServiceComplemento _serviceComplemento;
        private readonly IServiceReservaHabitaciones _serviceReservaHabitacion;
        private readonly IServiceReservaHuespedes _serviceReservaHuespedes;
        private readonly IServiceReservaComplementos _serviceReservaComplementos;
        private readonly ILogger<ServiceReserva> _logger;

        public ReservaController(
            IServiceReserva serviceReserva,
            IServiceCrucero serviceCrucero,
            IServiceCliente serviceCliente,
            IServiceBarcoHabitaciones serviceBarcoHabitaciones,
            IServiceUsuarioHuesped serviceUsuarioHuesped,
            IServiceComplemento serviceComplemento,
            IServiceReservaHabitaciones serviceReservaHabitacion,
            IServiceReservaHuespedes serviceReservaHuespedes,
            IServiceReservaComplementos serviceReservaComplementos,
            ILogger<ServiceReserva> logger)
        {
            _serviceReserva = serviceReserva;
            _serviceCrucero = serviceCrucero;
            _serviceCliente = serviceCliente;
            _serviceBarcoHabitaciones = serviceBarcoHabitaciones;
            _serviceUsuarioHuesped = serviceUsuarioHuesped;
            _serviceComplemento = serviceComplemento;
            _serviceReservaHabitacion = serviceReservaHabitacion;
            _serviceReservaHuespedes = serviceReservaHuespedes;
            _serviceReservaComplementos = serviceReservaComplementos;
            _logger = logger;
        }

        public async Task<IActionResult> Crear(int? id)
        {
            if (!int.TryParse(User.FindFirst("IdUsuario")?.Value, out int idUsuario))
                return Unauthorized();

            var crucero = await _serviceCrucero.FindByIdAsync(id!.Value);
            if (crucero == null) return NotFound();

            var usuarioDTO = await _serviceCliente.FindByIdAsync(idUsuario);
            var habitacionesDTO = await _serviceBarcoHabitaciones.GetHabitacionesByBarcoAsync(crucero.IdBarco);
            var huespedesDTO = await _serviceUsuarioHuesped.GetHuespedByUsuarioAsync(idUsuario);
            var complementoDTO = await _serviceComplemento.ListAsync();

            var reserva = new ReservaDTO
            {
                FechaReserva = DateOnly.FromDateTime(DateTime.Now),
                IdCrucero = crucero.Id,
                IdCruceroNavigation = crucero,
                IdUsuario = usuarioDTO.Id,
                IdUsuarioNavigation = usuarioDTO,
            };

            var viewModel = new ViewModelReserva
            {
                Reserva = reserva,
                Habitaciones = habitacionesDTO.ToList(),
                Huespedes = huespedesDTO.ToList(),
                Complementos = complementoDTO.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ReservaDTO reserva,string HabitacionesSeleccionadas,
                                                                  string HuespedesSeleccionados,
                                                                  string ComplementosSeleccionados,
                                                                  decimal? montoPagado)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Formulario inválido.");
                reserva.IdCruceroNavigation = await _serviceCrucero.FindByIdAsync(reserva.IdCrucero);
                var habitacionesDTO = await _serviceBarcoHabitaciones.GetHabitacionesByBarcoAsync(reserva.IdCruceroNavigation.IdBarco);
                var huespedesDTO = await _serviceUsuarioHuesped.GetHuespedByUsuarioAsync(reserva.IdUsuario);
                var complementoDTO = await _serviceComplemento.ListAsync();

                var viewModel = new ViewModelReserva
                {
                    Reserva = reserva,
                    Habitaciones = habitacionesDTO.ToList(),
                    Huespedes = huespedesDTO.ToList(),
                    Complementos = complementoDTO.ToList(),
                };
                return View(viewModel);
            }

            try
            {
                var habitacionesRaw = JsonSerializer.Deserialize<List<HabitacionSeleccionadaTemp>>(HabitacionesSeleccionadas);
                var huespedes = JsonSerializer.Deserialize<List<HuespedDTO>>(HuespedesSeleccionados);
                var complementos = JsonSerializer.Deserialize<List<ComplementoSimpleDTO>>(ComplementosSeleccionados);

                reserva.FechaReserva = DateOnly.FromDateTime(DateTime.Now);
                reserva.IdCruceroNavigation = await _serviceCrucero.FindByIdAsync(reserva.IdCrucero);
                reserva.Saldopendiente = reserva.Total - (montoPagado ?? 0);
                reserva.Estado = reserva.Saldopendiente == 0;

                reserva.IdCruceroNavigation = null;
                reserva.IdUsuarioNavigation = null;

                var nuevaReserva = await _serviceReserva.CreateAsync(reserva);
                int idReserva = nuevaReserva.Id;

                if (habitacionesRaw != null)
                {
                    foreach (var h in habitacionesRaw)
                    {
                        var idHabitacion = int.Parse(h.Id);
                        var reservaDTO = new ReservaHabitacionDTO
                        {
                            IdHabitacion = idHabitacion,
                            CantidadPasajerosHabitacion = h.Pasajeros,
                            IdReserva = idReserva
                        };

                        await _serviceReservaHabitacion.CreateAsync(reservaDTO);
                        await _serviceBarcoHabitaciones.UpdateAsync(
                            nuevaReserva.IdCruceroNavigation.IdBarco,
                            idHabitacion,
                            h.Cantidad);
                    }
                }

                if (huespedes != null)
                {
                    foreach (var h in huespedes)
                    {
                        await _serviceReservaHuespedes.CreateAsync(new ReservaHuespedDTO
                        {
                            IdReserva = idReserva,
                            IdHuesped = h.Id
                        });
                    }
                }

                if (complementos != null)
                {
                    foreach (var c in complementos)
                    {
                        await _serviceReservaComplementos.CreateAsync(idReserva, c.Id);
                    }
                }
                return RedirectToAction("IndexReserva");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la reserva.");
                ModelState.AddModelError("", "Ocurrió un error al guardar la reserva.");

                reserva.IdCruceroNavigation = await _serviceCrucero.FindByIdAsync(reserva.IdCrucero);
                var habitacionesDTO = await _serviceBarcoHabitaciones.GetHabitacionesByBarcoAsync(reserva.IdCruceroNavigation.IdBarco);
                var huespedesDTO = await _serviceUsuarioHuesped.GetHuespedByUsuarioAsync(reserva.IdUsuario);
                var complementoDTO = await _serviceComplemento.ListAsync();

                var viewModel = new ViewModelReserva
                {
                    Reserva = reserva,
                    Habitaciones = habitacionesDTO.ToList(),
                    Huespedes = huespedesDTO.ToList(),
                    Complementos = complementoDTO.ToList(),
                };

                return View(viewModel);
            }
        }

        
    }
}
