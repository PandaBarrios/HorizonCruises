using HorizonCruises.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.web.ViewModels
{
    public class ViewModelReserva
    {
        public ReservaDTO Reserva { get; set; } = new ReservaDTO();
        public List<BarcoHabitacionesDTO> Habitaciones { get; set; } = new();
        public List<UsuarioHuespedDTO> Huespedes { get; set; } = new();
        public List<ComplementoDTO> Complementos { get; set; } = new();
    }
}
