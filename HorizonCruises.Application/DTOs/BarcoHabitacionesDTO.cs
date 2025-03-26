using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record BarcoHabitacionesDTO
    {
        public int IdBarco { get; set; }
        public int IdHabitacion { get; set; }

        [Display(Name = "Total de Habitaciones Disponibles")]
        public int? TotalHabitacionesDisponibles { get; set; }
        public virtual Barco IdBarcoNavigation { get; set; } = null!;
        public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
    }
}
