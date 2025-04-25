using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Tarjeta
{
    public int Id { get; set; }

    public long? NumeroTarjeta { get; set; }

    public DateOnly? FechaCaducidad { get; set; }

    public int? Cvv { get; set; }

    public string? Titular { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
