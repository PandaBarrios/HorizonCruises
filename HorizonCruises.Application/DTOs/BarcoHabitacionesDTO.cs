using HorizonCruises.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace HorizonCruises.Application.DTOs
{
    public record BarcoHabitacionesDTO
    {
        public int IdBarco { get; set; }
        public int IdHabitacion { get; set; }

        [Display(Name = "Total de Habitaciones Disponibles")]
        public int? TotalHabitacionesDisponibles { get; set; }

        [JsonIgnore]
        [BindNever] 
        public HabitacionDTO IdHabitacionNavigation { get; set; } 

        [JsonIgnore]
        [BindNever]
        public BarcoDTO IdBarcoNavigation { get; set; }

    }
}
