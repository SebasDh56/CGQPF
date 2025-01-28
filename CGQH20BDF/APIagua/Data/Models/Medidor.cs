using System;
using System.Collections.Generic;

namespace APIagua.Data.Models;

public partial class Medidor
{
    public int IdMedidor { get; set; }

    public string NumeroSerie { get; set; } = null!;

    public DateOnly FechaInstalacion { get; set; }

    public string TipoMedidor { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public string? Estado { get; set; }

    public DateOnly? FechaUltimaRevision { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Incidencium> Incidencia { get; set; } = new List<Incidencium>();

    public virtual ICollection<Lectura> Lecturas { get; set; } = new List<Lectura>();
}
