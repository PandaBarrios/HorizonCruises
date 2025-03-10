using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Telefono
{
    public int Id { get; set; }

    public string? Numero { get; set; }

    public string? Descripcion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
