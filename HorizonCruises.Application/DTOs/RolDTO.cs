using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record RolDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public virtual List<Usuario> Usuario { get; set; } = new List<Usuario>();
    }
}
