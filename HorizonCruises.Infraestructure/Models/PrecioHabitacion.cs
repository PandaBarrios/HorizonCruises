using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class PrecioHabitacion
{
    public int Id { get; set; }

    public int IdCruceroFecha { get; set; }

    public int IdHabitacion { get; set; }

    public decimal? PrecioHabitacion1 { get; set; }

    public DateOnly? FechaLimitePrecio { get; set; }

    public virtual FechaCrucero IdCruceroFechaNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
