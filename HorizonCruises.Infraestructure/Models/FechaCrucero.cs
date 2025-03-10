using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class FechaCrucero
{
    public int Id { get; set; }

    public int? IdCrucero { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaLimitePago { get; set; }

    public virtual Crucero? IdCruceroNavigation { get; set; }

    public virtual ICollection<PrecioHabitacion> PrecioHabitacion { get; set; } = new List<PrecioHabitacion>();
}
