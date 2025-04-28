using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record ComplementoDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Descripcion { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        [Required]
        [Display(Name = "Tipo aplicación")]
        public bool AplicadoA { get; set; }

        public virtual List<ReservaComplemento>? ReservaComplemento { get; set; }
    }
}