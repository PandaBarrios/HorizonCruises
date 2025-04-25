using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public DateOnly? FechaReserva { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdCrucero { get; set; }

    public bool Estado { get; set; }

    public decimal? Saldopendiente { get; set; }

    public decimal? Iva { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Total { get; set; }

    public virtual Crucero? IdCruceroNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<ReservaComplemento> ReservaComplemento { get; set; } = new List<ReservaComplemento>();

    public virtual ICollection<ReservaHabitacion> ReservaHabitacion { get; set; } = new List<ReservaHabitacion>();

    public virtual ICollection<ReservaHuesped> ReservaHuesped { get; set; } = new List<ReservaHuesped>();


    public virtual ICollection<Huesped> IdHuesped { get; set; } = new List<Huesped>();
}
