using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class ReservaHuesped
{
    public int Id { get; set; }

    public int IdReservaEncabezado { get; set; }

    public int IdHuesped { get; set; }

    public virtual Huesped IdHuespedNavigation { get; set; } = null!;

    public virtual ReservaEncabezado IdReservaEncabezadoNavigation { get; set; } = null!;
}
