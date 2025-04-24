using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.web.ViewModels
{
    public class ViewModelHuesped
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre no debe contener números ni símbolos")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El apellido no debe contener números ni símbolos")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(ViewModelHuesped), nameof(ValidarFechaNacimiento))]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El pasaporte es obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "El pasaporte no debe contener símbolos")]
        public string Pasaporte { get; set; } = null!;

        [Required(ErrorMessage = "Debe seleccionar un género")]
        public bool? Genero { get; set; }

        public static ValidationResult? ValidarFechaNacimiento(DateOnly fecha, ValidationContext context)
        {
            if (fecha > DateOnly.FromDateTime(DateTime.Today))
            {
                return new ValidationResult("La fecha de nacimiento no puede ser mayor al día de hoy.");
            }
            return ValidationResult.Success;
        }
    }

}

