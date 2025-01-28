using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Lectura
{
    public int IdLectura { get; set; }

    public int? IdMedidor { get; set; }

    public DateOnly FechaLectura { get; set; }

    public decimal LecturaAnterior { get; set; }

    public decimal LecturaActual { get; set; }

    public decimal? Consumo { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Medidor? IdMedidorNavigation { get; set; }
}
