using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public class UsuarioHuespedDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        public int IdHuesped { get; set; }

        public virtual HuespedDTO IdHuespedNavigation { get; set; } = null!;

        public virtual ClienteDTO IdUsuarioNavigation { get; set; } = null!;
    }
}
