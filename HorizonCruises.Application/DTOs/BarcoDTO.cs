using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record BarcoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        [Display(Name = "Capacidad de Huéspedes")]
        public int? CapacidadHuespedes { get; set; }
        public virtual ICollection<BarcoHabitaciones> BarcoHabitaciones { get; set; } = new List<BarcoHabitaciones>();
        public virtual ICollection<Crucero> Crucero { get; set; } = new List<Crucero>();
    }
}
