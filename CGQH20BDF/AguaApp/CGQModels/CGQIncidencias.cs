using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaApp.Models
{
    public class CGQIncidencias
    {
        public int idIncidencia { get; set; }
        public int idUsuario { get; set; }
        public int idMedidor { get; set; }
        public string tipoIncidencia { get; set; }
        public string fechaReporte { get; set; }
        public string estado { get; set; }
        public string detalleResolucion { get; set; }
        public object idMedidorNavigation { get; set; }
        public object idUsuarioNavigation { get; set; }
    }
}
