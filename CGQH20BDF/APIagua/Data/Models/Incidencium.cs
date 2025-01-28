using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Incidencium
{
    public int IdIncidencia { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdMedidor { get; set; }

    public string TipoIncidencia { get; set; } = null!;

    public DateOnly FechaReporte { get; set; }

    public string? Estado { get; set; }

    public string? DetalleResolucion { get; set; }

    public virtual Medidor? IdMedidorNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
