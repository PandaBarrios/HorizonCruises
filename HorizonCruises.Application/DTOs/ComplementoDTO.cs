using HorizonCruises.Infraestructure.Models;

namespace HorizonCruises.Application.DTOs
{
    public record ComplementoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }

        public bool AplicadoA { get; set; }

        public virtual List<ReservaComplemento> ReservaComplemento { get; set; } = null!;
    }
}