using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public int? IdFactura { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal MontoPagado { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual ICollection<Recibo> Recibos { get; set; } = new List<Recibo>();
}
