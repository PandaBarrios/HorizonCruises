using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class UsuarioHuesped
{
    public int IdUsuario { get; set; }

    public int IdHuesped { get; set; }

    public int Id { get; set; }

    public virtual Huesped IdHuespedNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
