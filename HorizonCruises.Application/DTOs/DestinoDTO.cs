using HorizonCruises.Infraestructure.Models;

namespace HorizonCruises.Application.DTOs
{
    public record DestinoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public virtual List<PuertoDTO> Puerto { get; set; } = null!;
    }
}