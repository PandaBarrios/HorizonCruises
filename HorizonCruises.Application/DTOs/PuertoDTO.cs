using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record PuertoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        [Display(Name = "Destino")]
        public string Pais { get; set; } = null!;

        public int IdDestino { get; set; }

        public virtual DestinoDTO IdDestinoNavigation { get; set; } = null!;

        public virtual List<ItinerarioDTO> Itinerario { get; set; } = null!;
    }
}
