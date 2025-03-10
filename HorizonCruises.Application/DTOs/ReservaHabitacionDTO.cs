using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record ReservaHabitacionDTO
    {
        [Display(Name = "Reserva")]
        public int IdReserva { get; set; }

        [Display(Name = "Habitacion")]
        public int IdHabitacion { get; set; }

        public int CantidadPasajerosHabitacion { get; set; }

        public virtual HabitacionDTO IdHabitacionNavigation { get; set; } = null!;

        public virtual ReservaDTO IdReservaNavigation { get; set; } = null!;

    }
}