using System;
using System.Collections.Generic;
using HorizonCruises.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HorizonCruises.Infraestructure.Data;

public partial class HorizonCruisesContext : DbContext
{
    public HorizonCruisesContext(DbContextOptions<HorizonCruisesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barco> Barco { get; set; }

    public virtual DbSet<BarcoHabitaciones> BarcoHabitaciones { get; set; }

    public virtual DbSet<Complemento> Complemento { get; set; }

    public virtual DbSet<Crucero> Crucero { get; set; }

    public virtual DbSet<Destino> Destino { get; set; }

    public virtual DbSet<FechaCrucero> FechaCrucero { get; set; }

    public virtual DbSet<Habitacion> Habitacion { get; set; }

    public virtual DbSet<Huesped> Huesped { get; set; }

    public virtual DbSet<Itinerario> Itinerario { get; set; }

    public virtual DbSet<PrecioHabitacion> PrecioHabitacion { get; set; }

    public virtual DbSet<Puerto> Puerto { get; set; }

    public virtual DbSet<Reserva> Reserva { get; set; }

    public virtual DbSet<ReservaComplemento> ReservaComplemento { get; set; }

    public virtual DbSet<ReservaHabitacion> ReservaHabitacion { get; set; }
    //public virtual DbSet<ReservaHuesped> ReservaHuesped { get; set; }
    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Tarjeta> Tarjeta { get; set; }

    public virtual DbSet<Telefono> Telefono { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioHuesped> UsuarioHuesped { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AI");

        modelBuilder.Entity<Barco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Barco__3213E83F0231B40D");

            entity.HasIndex(e => e.Nombre, "UQ__Barco__72AFBCC60189147B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CapacidadHuespedes).HasColumnName("capacidad_huespedes");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<BarcoHabitaciones>(entity =>
        {
            entity.HasKey(e => new { e.IdBarco, e.IdHabitacion }).HasName("PK__Barco_Ha__48DF2444AE0BF6D5");

            entity.ToTable("Barco_Habitaciones");

            entity.Property(e => e.IdBarco).HasColumnName("id_barco");
            entity.Property(e => e.IdHabitacion).HasColumnName("id_habitacion");
            entity.Property(e => e.TotalHabitacionesDisponibles).HasColumnName("total_habitaciones_disponibles");

            entity.HasOne(d => d.IdBarcoNavigation).WithMany(p => p.BarcoHabitaciones)
                .HasForeignKey(d => d.IdBarco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Barco_Hab__id_ba__00200768");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.BarcoHabitaciones)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Barco_Hab__id_ha__01142BA1");
        });

        modelBuilder.Entity<Complemento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compleme__3213E83FB7294EAC");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AplicadoA).HasColumnName("aplicado_a");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Crucero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Crucero__3213E83FEF5E44FD");

            entity.HasIndex(e => e.Nombre, "UQ__Crucero__72AFBCC6435EE4BA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadDias).HasColumnName("cantidad_dias");
            entity.Property(e => e.IdBarco).HasColumnName("id_barco");
            entity.Property(e => e.Imagen).HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdBarcoNavigation).WithMany(p => p.Crucero)
                .HasForeignKey(d => d.IdBarco)
                .HasConstraintName("FK__Crucero__id_barc__04E4BC85");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Destino__3213E83FCD4D0DF5");

            entity.HasIndex(e => e.Nombre, "UQ__Destino__72AFBCC6EA463B6A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<FechaCrucero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fecha_Cr__3213E83F4836A2F6");

            entity.ToTable("Fecha_Crucero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaLimitePago).HasColumnName("fecha_limite_pago");
            entity.Property(e => e.IdCrucero).HasColumnName("id_crucero");

            entity.HasOne(d => d.IdCruceroNavigation).WithMany(p => p.FechaCrucero)
                .HasForeignKey(d => d.IdCrucero)
                .HasConstraintName("FK__Fecha_Cru__id_cr__0B91BA14");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__3213E83F2F2C101F");

            entity.HasIndex(e => e.Nombre, "UQ__Habitaci__72AFBCC6A8FFAC55").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadMaximaHuespedes).HasColumnName("cantidad_maxima_huespedes");
            entity.Property(e => e.CantidadMinimaHuespedes).HasColumnName("cantidad_minima_huespedes");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Tamano).HasColumnName("tamano");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Huesped>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo).HasColumnName("correo");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero).HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Pasaporte)
                .HasMaxLength(50)
                .HasColumnName("pasaporte");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => new { e.IdCrucero, e.IdPuerto });

            entity.Property(e => e.IdCrucero).HasColumnName("id_crucero");
            entity.Property(e => e.IdPuerto).HasColumnName("id_puerto");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.IdCruceroNavigation).WithMany(p => p.Itinerario)
                .HasForeignKey(d => d.IdCrucero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Itinerari__id_cr__07C12930");

            entity.HasOne(d => d.IdPuertoNavigation).WithMany(p => p.Itinerario)
                .HasForeignKey(d => d.IdPuerto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Itinerari__id_pu__08B54D69");
        });

        modelBuilder.Entity<PrecioHabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Precio_Habitacion_1");

            entity.ToTable("Precio_Habitacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaLimitePrecio).HasColumnName("fecha_limite_precio");
            entity.Property(e => e.IdCruceroFecha).HasColumnName("id_crucero_fecha");
            entity.Property(e => e.IdHabitacion).HasColumnName("id_habitacion");
            entity.Property(e => e.PrecioHabitacion1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_habitacion");

            entity.HasOne(d => d.IdCruceroFechaNavigation).WithMany(p => p.PrecioHabitacion)
                .HasForeignKey(d => d.IdCruceroFecha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Precio_Habitacion_Fecha_Crucero");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.PrecioHabitacion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Precio_Habitacion_Habitacion");
        });

        modelBuilder.Entity<Puerto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Puerto__3213E83FB6B72E42");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDestino).HasColumnName("id_destino");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(200)
                .HasColumnName("pais");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.Puerto)
                .HasForeignKey(d => d.IdDestino)
                .HasConstraintName("FK__Puerto__id_desti__73BA3083");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Factura_Encabezado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaReserva).HasColumnName("fecha_reserva");
            entity.Property(e => e.IdCrucero).HasColumnName("id_crucero");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("iva");
            entity.Property(e => e.Saldopendiente)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("saldopendiente");
            entity.Property(e => e.SubTotal)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("subTotal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdCruceroNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.IdCrucero)
                .HasConstraintName("FK_Reserva_Encabezado_Crucero");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Factura_Encabezado_Usuario");

            entity.HasMany(d => d.IdHuesped).WithMany(p => p.IdReserva)
                .UsingEntity<Dictionary<string, object>>(
                    "ReservaHuesped",
                    r => r.HasOne<Huesped>().WithMany()
                        .HasForeignKey("IdHuesped")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Reserva_Huesped_Huesped"),
                    l => l.HasOne<Reserva>().WithMany()
                        .HasForeignKey("IdReserva")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Reserva_Huesped_Reserva"),
                    j =>
                    {
                        j.HasKey("IdReserva", "IdHuesped");
                        j.ToTable("Reserva_Huesped");
                        j.IndexerProperty<int>("IdReserva").HasColumnName("Id_reserva");
                        j.IndexerProperty<int>("IdHuesped").HasColumnName("Id_huesped");
                    });
        });

        modelBuilder.Entity<ReservaComplemento>(entity =>
        {
            entity.HasKey(e => new { e.IdReserva, e.IdComplemento });

            entity.ToTable("Reserva_Complemento");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.IdComplemento).HasColumnName("id_complemento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");

            entity.HasOne(d => d.IdComplementoNavigation).WithMany(p => p.ReservaComplemento)
                .HasForeignKey(d => d.IdComplemento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Complemento_Complemento");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.ReservaComplemento)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Complemento_Reserva");
        });

        modelBuilder.Entity<ReservaHabitacion>(entity =>
        {
            entity.HasKey(e => new { e.IdReserva, e.IdHabitacion }).HasName("PK_Reserva_Habitacion_1");

            entity.ToTable("Reserva_Habitacion");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.IdHabitacion).HasColumnName("id_habitacion");
            entity.Property(e => e.CantidadPasajerosHabitacion).HasColumnName("cantidad_pasajeros_habitacion");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.ReservaHabitacion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Habitacion_Habitacion");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.ReservaHabitacion)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Habitacion_Reserva");
        });

        //modelBuilder.Entity<ReservaHuesped>(entity =>
        //{
        //    entity.ToTable("Reserva_Huesped");

        //    entity.HasKey(e => new { e.IdReserva, e.IdHuesped });

        //    entity.Property(e => e.IdReserva).HasColumnName("Id_reserva");
        //    entity.Property(e => e.IdHuesped).HasColumnName("Id_huesped");

        //    entity.HasOne(e => e.IdReservaNavigation)
        //        .WithMany(r => r.ReservaHuesped)
        //        .HasForeignKey(e => e.IdReserva)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    entity.HasOne(e => e.IdHuespedNavigation)
        //        .WithMany(h => h.ReservaHuesped)
        //        .HasForeignKey(e => e.IdHuesped)
        //        .OnDelete(DeleteBehavior.Cascade);
        //});


        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83F98C8EA01");

            entity.HasIndex(e => e.Nombre, "UQ__Rol__72AFBCC6FF3CD7DF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cvv).HasColumnName("CVV");
            entity.Property(e => e.FechaCaducidad).HasColumnName("fecha_caducidad");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NumeroTarjeta).HasColumnName("numero_tarjeta");
            entity.Property(e => e.Titular)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("titular");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tarjeta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Tarjeta_Usuario");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .HasColumnName("numero");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Telefono)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Telefono_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F66699654");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuario__5B8A06822AB3B36C").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__Usuario__72AFBCC6EA9D7B08").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(200)
                .HasColumnName("contrasena");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(200)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(200)
                .HasColumnName("pais");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__id_rol__6E01572D");
        });

        modelBuilder.Entity<UsuarioHuesped>(entity =>
        {
            entity.ToTable("Usuario_Huesped");

            entity.Property(e => e.IdHuesped).HasColumnName("id_huesped");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdHuespedNavigation).WithMany(p => p.UsuarioHuesped)
                .HasForeignKey(d => d.IdHuesped)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Huesped_Huesped");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioHuesped)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Huesped_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
