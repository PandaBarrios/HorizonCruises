using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Barco
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int CapacidadHuespedes { get; set; }

    public virtual ICollection<BarcoHabitaciones> BarcoHabitaciones { get; set; } = new List<BarcoHabitaciones>();

    public virtual ICollection<Crucero> Crucero { get; set; } = new List<Crucero>();
}
