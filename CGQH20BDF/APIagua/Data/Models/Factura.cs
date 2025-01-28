using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdLectura { get; set; }

    public DateOnly FechaEmision { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public decimal MontoTotal { get; set; }

    public string? Estado { get; set; }

    public string? DetalleConsumo { get; set; }

    public virtual Lectura? IdLecturaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
