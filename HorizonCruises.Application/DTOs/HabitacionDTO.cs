using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record HabitacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int CantidadMinimaHuespedes { get; set; }
        public int CantidadMaximaHuespedes { get; set; }
        public double Tamano { get; set; } 
        public string Tipo { get; set; } = null!;
        public virtual List<BarcoHabitaciones> BarcoHabitaciones { get; set; } = null!;
        public virtual List<PrecioHabitacion> PrecioHabitacion { get; set; } = null!;
        public virtual List<ReservaHabitacion> ReservaHabitacion { get; set; } = null!;
    }
}
