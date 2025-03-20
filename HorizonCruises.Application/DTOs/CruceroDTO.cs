using HorizonCruises.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record CruceroDTO
    {
        [Display(Name = "Identificador Crucero")]
        public int Id { get; set; }
        
        [Display(Name = "Nombre Crucero")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Imagen Crucero")]
        public byte[] Imagen { get; set; } = null!;

        [Display(Name = "Cantidad de dias")]
        public int CantidadDias { get; set; }

        [Display(Name = "Barco")]
        public int IdBarco { get; set; }

        public virtual List<FechaCruceroDTO> FechaCrucero { get; set; } = null!;
        public virtual BarcoDTO IdBarcoNavigation { get; set; } = null!;
        public virtual List<ItinerarioDTO> Itinerario { get; set; } = null!;
        public virtual List<ReservaDTO> Reserva { get; set; }= null!;
    }
}
