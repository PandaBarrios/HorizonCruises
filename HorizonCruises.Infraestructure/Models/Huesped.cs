using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Huesped
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Correo { get; set; }

    public string Pasaporte { get; set; } = null!;

    public bool? Genero { get; set; }

    public virtual ICollection<Reserva> IdReserva { get; set; } = new List<Reserva>();
}
