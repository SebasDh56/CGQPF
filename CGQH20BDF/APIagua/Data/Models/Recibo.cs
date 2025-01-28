using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Recibo
{
    public int IdRecibo { get; set; }

    public int? IdPago { get; set; }

    public DateOnly FechaEmision { get; set; }

    public decimal Monto { get; set; }

    public string MetodoEntrega { get; set; } = null!;

    public virtual Pago? IdPagoNavigation { get; set; }
}
