using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record HabitacionDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Cantidad Mínima de Huéspedes")]
        public int CantidadMinimaHuespedes { get; set; }

        [Display(Name = "Cantidad Máxima de Huéspedes")]
        public int CantidadMaximaHuespedes { get; set; }

        [Display(Name = "Tamaño (m²)")]
        public double Tamano { get; set; }

        [Display(Name = "Tipo de Habitación")]
        public string Tipo { get; set; } = null!;
        public virtual List<BarcoHabitaciones> BarcoHabitaciones { get; set; } = new List<BarcoHabitaciones>();

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public virtual List<PrecioHabitacion> PrecioHabitacion { get; set; } = new List<PrecioHabitacion>();
        public virtual List<ReservaHabitacion> ReservaHabitacion { get; set; } = new List<ReservaHabitacion>();
    }
}
