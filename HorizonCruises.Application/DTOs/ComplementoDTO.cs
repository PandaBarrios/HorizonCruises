using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HorizonCruises.Application.DTOs
{
    public record ComplementoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [Required]
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = null!;

        [Required]
        [JsonPropertyName("precio")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        [Required]
        [Display(Name = "Tipo aplicación")]
        [JsonPropertyName("aplicadoA")]
        public bool AplicadoA { get; set; }

        public virtual List<ReservaComplemento>? ReservaComplemento { get; set; }
    }
}