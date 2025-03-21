using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record  FechaCruceroDTO
    {
        public int Id { get; set; }

        public int IdCrucero { get; set; }
        [Display(Name = "Inico del evento")]
        public DateOnly FechaInicio { get; set; }
        [Display(Name = "Ultimo dia de reserva")]
        public DateOnly FechaLimitePago { get; set; }

        public virtual CruceroDTO IdCruceroNavigation { get; set; } = null!;

        public virtual List<PrecioHabitacionDTO> PrecioHabitacion { get; set; } = null!;
    }
}