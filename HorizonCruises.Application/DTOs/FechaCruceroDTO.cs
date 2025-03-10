using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record  FechaCruceroDTO
    {
        public int Id { get; set; }

        public int IdCrucero { get; set; }
        [Display(Name = "Inico del evento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaInicio { get; set; }
        [Display(Name = "Ultimodia de reserva")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaLimitePago { get; set; }

        public virtual CruceroDTO IdCruceroNavigation { get; set; } = null!;

        public virtual List<PrecioHabitacion> PrecioHabitacion { get; set; } = null!;
    }
}