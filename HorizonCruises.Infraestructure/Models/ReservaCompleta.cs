using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class ReservaCompleta
{
    public int IdReservaEncabezado { get; set; }

    public int IdHabitacion { get; set; }

    public int CantidadPasajerosTotales { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Reserva IdReservaEncabezadoNavigation { get; set; } = null!;
}
