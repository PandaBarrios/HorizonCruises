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

        public string Nombre { get; set; } = null!;

        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string CorreoElectronico { get; set; } = null!;

        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaNacimiento { get; set; }

        public string Pais { get; set; } = null!;

        [Display(Name = "Contraseña")]
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
