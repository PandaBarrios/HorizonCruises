using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Puerto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Pais { get; set; }

    public int? IdDestino { get; set; }

    public virtual Destino? IdDestinoNavigation { get; set; }

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();
}
