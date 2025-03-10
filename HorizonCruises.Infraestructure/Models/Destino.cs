using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Destino
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Puerto> Puerto { get; set; } = new List<Puerto>();
}
