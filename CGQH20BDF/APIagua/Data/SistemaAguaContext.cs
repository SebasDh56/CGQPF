using System;
using System.Collections.Generic;
using APIagua.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APIagua.Data;

public partial class SistemaAguaContext : DbContext
{
    public SistemaAguaContext()
    {
    }

    public SistemaAguaContext(DbContextOptions<SistemaAguaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Incidencium> Incidencia { get; set; }

    public virtual DbSet<Lectura> Lecturas { get; set; }

    public virtual DbSet<Medidor> Medidors { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Recibo> Recibos { get; set; }

    public virtual DbSet<Tarifa> Tarifas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaAgua");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__88B5139482028240");

            entity.ToTable("Empleado");

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cargo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.FechaContratacion).HasColumnName("fecha_contratacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__6C08ED53DA360178");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.DetalleConsumo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detalle_consumo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.IdLectura).HasColumnName("id_lectura");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_total");

            entity.HasOne(d => d.IdLecturaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdLectura)
                .HasConstraintName("FK__Factura__id_lect__3A81B327");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Factura__id_usua__398D8EEE");
        });

        modelBuilder.Entity<Incidencium>(entity =>
        {
            entity.HasKey(e => e.IdIncidencia).HasName("PK__Incidenc__D7757E76F18E7287");

            entity.Property(e => e.IdIncidencia).HasColumnName("id_incidencia");
            entity.Property(e => e.DetalleResolucion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("detalle_resolucion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaReporte).HasColumnName("fecha_reporte");
            entity.Property(e => e.IdMedidor).HasColumnName("id_medidor");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.TipoIncidencia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_incidencia");

            entity.HasOne(d => d.IdMedidorNavigation).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.IdMedidor)
                .HasConstraintName("FK__Incidenci__id_me__4D94879B");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Incidenci__id_us__4CA06362");
        });

        modelBuilder.Entity<Lectura>(entity =>
        {
            entity.HasKey(e => e.IdLectura).HasName("PK__Lectura__E96875968D0CDC28");

            entity.ToTable("Lectura");

            entity.Property(e => e.IdLectura).HasColumnName("id_lectura");
            entity.Property(e => e.Consumo)
                .HasComputedColumnSql("([lectura_actual]-[lectura_anterior])", false)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("consumo");
            entity.Property(e => e.Estado)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente de revisión")
                .HasColumnName("estado");
            entity.Property(e => e.FechaLectura).HasColumnName("fecha_lectura");
            entity.Property(e => e.IdMedidor).HasColumnName("id_medidor");
            entity.Property(e => e.LecturaActual)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("lectura_actual");
            entity.Property(e => e.LecturaAnterior)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("lectura_anterior");

            entity.HasOne(d => d.IdMedidorNavigation).WithMany(p => p.Lecturas)
                .HasForeignKey(d => d.IdMedidor)
                .HasConstraintName("FK__Lectura__id_medi__31EC6D26");
        });

        modelBuilder.Entity<Medidor>(entity =>
        {
            entity.HasKey(e => e.IdMedidor).HasName("PK__Medidor__E67D5EC870BB4C26");

            entity.ToTable("Medidor");

            entity.HasIndex(e => e.NumeroSerie, "UQ__Medidor__D8D7353C4C8C4AAE").IsUnique();

            entity.Property(e => e.IdMedidor).HasColumnName("id_medidor");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estado");
            entity.Property(e => e.FechaInstalacion).HasColumnName("fecha_instalacion");
            entity.Property(e => e.FechaUltimaRevision).HasColumnName("fecha_ultima_revision");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NumeroSerie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_serie");
            entity.Property(e => e.TipoMedidor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_medidor");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ubicacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Medidors)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Medidor__id_usua__2D27B809");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__0941B07448A5275C");

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.MontoPagado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_pagado");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK__Pago__id_factura__403A8C7D");
        });

        modelBuilder.Entity<Recibo>(entity =>
        {
            entity.HasKey(e => e.IdRecibo).HasName("PK__Recibo__1F2CC1BA2ED0A969");

            entity.ToTable("Recibo");

            entity.Property(e => e.IdRecibo).HasColumnName("id_recibo");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.MetodoEntrega)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("metodo_entrega");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Recibos)
                .HasForeignKey(d => d.IdPago)
                .HasConstraintName("FK__Recibo__id_pago__440B1D61");
        });

        modelBuilder.Entity<Tarifa>(entity =>
        {
            entity.HasKey(e => e.IdTarifa).HasName("PK__Tarifa__747D038958C74CCD");

            entity.ToTable("Tarifa");

            entity.Property(e => e.IdTarifa).HasColumnName("id_tarifa");
            entity.Property(e => e.FechaVigencia).HasColumnName("fecha_vigencia");
            entity.Property(e => e.PrecioPorM3)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_por_m3");
            entity.Property(e => e.RangoConsumoMaximo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("rango_consumo_maximo");
            entity.Property(e => e.RangoConsumoMinimo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("rango_consumo_minimo");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD7C0F1DAC");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.EstadoServicio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estado_servicio");
            entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
