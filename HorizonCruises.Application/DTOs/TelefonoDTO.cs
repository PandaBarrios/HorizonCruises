using HorizonCruises.Infraestructure.Models;

namespace HorizonCruises.Application.DTOs
{
    public record TelefonoDTO
    {
        public int Id { get; set; }

        public string? Numero { get; set; } 

        public string? Descripcion { get; set; } = null!;

        public int? IdUsuario { get; set; }

        public virtual ClienteDTO IdUsuarioNavigation { get; set; } = null!;
    }
}