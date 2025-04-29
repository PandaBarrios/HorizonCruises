using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record ReservaHuespedDTO
    {
        public int IdReserva{ get; set; }

        public int IdHuesped { get; set; }

        public virtual HuespedDTO IdHuespedNavigation { get; set; } = null!;

        public virtual ReservaDTO IdReservaNavigation { get; set; } = null!;
    }
}
