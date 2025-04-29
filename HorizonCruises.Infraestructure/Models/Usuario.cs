using System;
using System.Collections.Generic;

namespace HorizonCruises.Infraestructure.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoElectronico { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Pais { get; set; }

    public string? Contrasena { get; set; }

    public int? IdRol { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Reserva> Reserva { get; set; } = new List<Reserva>();

    public virtual ICollection<Tarjeta> Tarjeta { get; set; } = new List<Tarjeta>();

    public virtual ICollection<Telefono> Telefono { get; set; } = new List<Telefono>();

    public virtual ICollection<UsuarioHuesped> UsuarioHuesped { get; set; } = new List<UsuarioHuesped>();
}
