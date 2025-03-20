using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record ItinerarioDTO
    {
        [Display(Name = "Crucero")]
        public int IdCrucero { get; set; }

        [Display(Name = "Puerto")]
        public int IdPuerto { get; set; }

        public string Descripcion { get; set; } = null!;

        [Display(Name = "Día")]
        public int Orden { get; set; }

        public virtual CruceroDTO IdCruceroNavigation { get; set; } = null!;
        public virtual PuertoDTO IdPuertoNavigation { get; set; } = null!;
    }
}
