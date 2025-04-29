using HorizonCruises.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonCruises.Application.DTOs
{
    public record ClienteDTO 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string CorreoElectronico { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        public string Pais { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;

        public int IdRol { get; set; }

        public virtual RolDTO? IdRolNavigation { get; set; }

        public virtual List<ReservaDTO>? Reserva { get; set; }

        public virtual List<TarjetaDTO>? Tarjeta { get; set; }

        public virtual List<TelefonoDTO>? Telefono { get; set; }
        public virtual List<UsuarioHuespedDTO>? UsuarioHuesped { get; set; }
    }
}
