using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Crucero
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public byte[]? Imagen { get; set; }

    public int? CantidadDias { get; set; }

    public int? IdBarco { get; set; }

    public virtual ICollection<FechaCrucero> FechaCrucero { get; set; } = new List<FechaCrucero>();

    public virtual Barco? IdBarcoNavigation { get; set; }

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();

    public virtual ICollection<Reserva> Reserva { get; set; } = new List<Reserva>();
}
