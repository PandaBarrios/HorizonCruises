using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Itinerario
{
    public int IdCrucero { get; set; }

    public int IdPuerto { get; set; }

    public string? Descripcion { get; set; }

    public int Orden { get; set; }

    public virtual Crucero IdCruceroNavigation { get; set; } = null!;

    public virtual Puerto IdPuertoNavigation { get; set; } = null!;
}
