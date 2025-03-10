using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record ReservaComplementoDTO
    {
        [Display(Name = "Reserva")]
        public int IdReserva { get; set; }

        [Display(Name = "Complemento")]
        public int IdComplemento { get; set; }

        public int Cantidad { get; set; }

        public virtual ComplementoDTO IdComplementoNavigation { get; set; } = null!;

        public virtual ReservaDTO IdReservaNavigation { get; set; } = null!;
    }
}