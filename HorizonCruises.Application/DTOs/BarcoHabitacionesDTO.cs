using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record BarcoHabitacionesDTO
    {
        public int IdBarco { get; set; }
        public int IdHabitacion { get; set; }
        public int? TotalHabitacionesDisponibles { get; set; }
        public virtual Barco IdBarcoNavigation { get; set; } = null!;
        public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
    }
}
