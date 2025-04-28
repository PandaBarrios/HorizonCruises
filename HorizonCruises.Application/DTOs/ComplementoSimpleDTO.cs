using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public class ComplementoSimpleDTO
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }
        [JsonPropertyName("aplicadoA")]
        public bool AplicaA { get; set; }
        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

    }
}
