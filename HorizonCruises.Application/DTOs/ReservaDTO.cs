using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record ReservaDTO
    {
        public int Id { get; set; }

        [Display(Name = "Fecha de reserva")]
        public DateOnly FechaReserva { get; set; }

        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Crucero")]
        public int IdCrucero { get; set; }

        public bool Estado { get; set; }

        [Display(Name = "Saldo Pendiente")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Saldopendiente { get; set; }

        [Display(Name = "Impuesto")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Iva { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Total { get; set; }

        public virtual List<ReservaHabitacionDTO>? ReservaHabitacion { get; set; }

        public virtual List<ReservaComplementoDTO>? ReservaComplemento { get; set; }

        public virtual List<HuespedDTO>? IdHuesped { get; set; }

        public virtual CruceroDTO? IdCruceroNavigation { get; set; }

        public virtual ClienteDTO? IdUsuarioNavigation { get; set; }

    }
}
