using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Tarifa
{
    public int IdTarifa { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public decimal RangoConsumoMinimo { get; set; }

    public decimal RangoConsumoMaximo { get; set; }

    public decimal PrecioPorM3 { get; set; }

    public DateOnly FechaVigencia { get; set; }
}
