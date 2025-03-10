using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class EstadoReserva
{
    public int Id { get; set; }

    public bool PendientePago { get; set; }

    public decimal? SaldoPendiente { get; set; }

    public virtual ICollection<Reserva> Reserva { get; set; } = new List<Reserva>();
}
