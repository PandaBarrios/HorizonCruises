using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class ReservaHuesped
{
    public int IdReserva { get; set; }

    public int IdHuesped { get; set; }

    public virtual Huesped IdHuespedNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
