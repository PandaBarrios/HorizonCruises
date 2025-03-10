using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record TarjetaDTO
    {
        public int Id { get; set; }

        public long NumeroTarjeta { get; set; }

        [Display(Name = "Fecha de Caducidad")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaCaducidad { get; set; }

        [Display(Name = "Codigo cvv")]
        public int Cvv { get; set; }

        public string Titular { get; set; } = null!;

        public int IdUsuario { get; set; }
        
        [Display(Name = "Cliente")]
        public virtual ClienteDTO IdUsuarioNavigation { get; set; } = null!;
    }
}
