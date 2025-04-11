using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record ComplementoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        public bool AplicadoA { get; set; }

        public virtual List<ReservaComplemento> ReservaComplemento { get; set; } = null!;
    }
}