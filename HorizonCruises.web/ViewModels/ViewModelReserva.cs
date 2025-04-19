using HorizonCruises.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HorizonCruises.web.ViewModels
{
    public class ViewModelReserva
    {
        public ReservaDTO Reserva { get; set; } = new ReservaDTO();
        public List<BarcoHabitacionesDTO> Habitaciones { get; set; } = new();
    }
}
