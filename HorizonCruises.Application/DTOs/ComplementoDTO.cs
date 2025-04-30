using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HorizonCruises.Application.DTOs
{
    public record ComplementoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "El precio debe estar entre 0.01 y 99999.99.")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe especificar el tipo de aplicación.")]
        [Display(Name = "Tipo de aplicación")]
        [JsonPropertyName("aplicadoA")]
        public bool AplicadoA { get; set; }

        public virtual List<ReservaComplemento>? ReservaComplemento { get; set; }
    }
}