﻿using HorizonCruises.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.Application.DTOs
{
    public record PrecioHabitacionDTO
    {
        public int Id { get; set; }

        public int IdCruceroFecha { get; set; }

        public int IdHabitacion { get; set; }

        public decimal? PrecioHabitacion1 { get; set; }

        [Display(Name = "Fecha Limite de Precios")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly FechaLimitePrecio { get; set; }

        public virtual FechaCruceroDTO IdCruceroFechaNavigation { get; set; } = null!;

        public virtual HabitacionDTO IdHabitacionNavigation { get; set; } = null!;
    }
}