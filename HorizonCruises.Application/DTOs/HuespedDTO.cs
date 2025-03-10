using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record HuespedDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        public string Correo { get; set; } = null!;

        public string Pasaporte { get; set; } = null!;

        public bool Genero { get; set; }

        public virtual List<ReservaDTO> Reserva { get; set; } = null!;
    }
}