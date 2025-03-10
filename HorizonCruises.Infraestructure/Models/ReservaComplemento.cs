using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class ReservaComplemento
{
    public int IdReserva { get; set; }

    public int IdComplemento { get; set; }

    public int Cantidad { get; set; }

    public virtual Complemento IdComplementoNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
