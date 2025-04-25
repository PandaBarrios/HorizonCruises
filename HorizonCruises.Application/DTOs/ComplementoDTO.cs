using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HorizonCruises.Application.DTOs
{
    public record ComplementoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = null!;

        [JsonPropertyName("precio")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }
        
        [JsonPropertyName("aplicadoA")]
        public bool AplicadoA { get; set; }

        public virtual List<ReservaComplemento> ReservaComplemento { get; set; } = null!;
    }
}