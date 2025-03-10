using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
