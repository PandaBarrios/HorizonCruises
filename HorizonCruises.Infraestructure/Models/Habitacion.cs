using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Habitacion
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? CantidadMinimaHuespedes { get; set; }

    public int? CantidadMaximaHuespedes { get; set; }

    public double? Tamano { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<BarcoHabitaciones> BarcoHabitaciones { get; set; } = new List<BarcoHabitaciones>();

    public virtual ICollection<PrecioHabitacion> PrecioHabitacion { get; set; } = new List<PrecioHabitacion>();

    public virtual ICollection<ReservaHabitacion> ReservaHabitacion { get; set; } = new List<ReservaHabitacion>();
}
