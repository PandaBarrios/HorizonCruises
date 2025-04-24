using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class BarcoHabitaciones
{
    public int IdBarco { get; set; }

    public int IdHabitacion { get; set; }

    public int? TotalHabitacionesDisponibles { get; set; }

    public virtual Barco IdBarcoNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
