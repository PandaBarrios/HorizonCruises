using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public class HabitacionSeleccionadaTemp
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        [JsonPropertyName("pasajeros")]
        public int Pasajeros { get; set; }
    }
}
