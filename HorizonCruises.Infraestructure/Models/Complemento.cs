using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Complemento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public bool? AplicadoA { get; set; }

    public virtual ICollection<ReservaComplemento> ReservaComplemento { get; set; } = new List<ReservaComplemento>();
}
