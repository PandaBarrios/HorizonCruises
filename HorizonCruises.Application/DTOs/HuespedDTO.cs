using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HorizonCruises.Application.DTOs
{
    public record HuespedDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; } = null!;

        [JsonPropertyName("fecha")]
        public DateOnly FechaNacimiento { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("pasaporte")]
        public string Pasaporte { get; set; } = null!;

        [JsonPropertyName("genero")]
        public bool Genero { get; set; }

        public virtual List<ReservaDTO> Reserva { get; set; } = null!;
        public virtual List<UsuarioHuespedDTO> UsuarioHuesped { get; set; } = null!;
    }
}