using HorizonCruises.Infraestructure.Models;

namespace HorizonCruises.Application.DTOs
{
    public record PrecioHabitacionDTO
    {
        public int IdCruceroFecha { get; set; }

        public int IdHabitacion { get; set; }

        public decimal PrecioHabitacion1 { get; set; }

        public virtual FechaCruceroDTO IdCruceroFechaNavigation { get; set; } = null!;

        public virtual HabitacionDTO IdHabitacionNavigation { get; set; } = null!;
    }
}