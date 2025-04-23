using HorizonCruises.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.web.ViewModels
{
    public class ViewModelReserva
    {
        public ReservaDTO Reserva { get; set; } = new ReservaDTO();

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public List<BarcoHabitacionesDTO> Habitaciones { get; set; } = new();
    }
}
