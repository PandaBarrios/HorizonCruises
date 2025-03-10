using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class ReservaEncabezado
{
    public int Id { get; set; }

    public DateOnly? FechaReserva { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdCrucero { get; set; }

    public int? IdEstadoReserva { get; set; }

    public virtual Crucero? IdCruceroNavigation { get; set; }

    public virtual EstadoReserva? IdEstadoReservaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

}
