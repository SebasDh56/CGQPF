using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public string? EstadoServicio { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Incidencium> Incidencia { get; set; } = new List<Incidencium>();

    public virtual ICollection<Medidor> Medidors { get; set; } = new List<Medidor>();
}
