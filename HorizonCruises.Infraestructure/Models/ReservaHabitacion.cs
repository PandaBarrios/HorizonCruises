using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class ReservaHabitacion
{
    public int IdReserva { get; set; }

    public int IdHabitacion { get; set; }

    public int CantidadPasajerosHabitacion { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
