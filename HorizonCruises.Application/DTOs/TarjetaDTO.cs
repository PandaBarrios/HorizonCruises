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

        [Display(Name = "Número de tarjeta")]
        [Required]
        [Range(1000000000000000, 9999999999999999, ErrorMessage = "El número debe tener 16 dígitos")]
        public long NumeroTarjeta { get; set; }

        [Display(Name = "Fecha de caducidad")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaCaducidad { get; set; }

        [Display(Name = "CVV")]
        [Required]
        [Range(100, 999, ErrorMessage = "El CVV debe ser de 3 dígitos.")]
        public int Cvv { get; set; }

        [Required]
        public string Titular { get; set; } = null!;

        public int IdUsuario { get; set; }
        
        [Display(Name = "Cliente")]
        public virtual ClienteDTO? IdUsuarioNavigation { get; set; }
    }
}
