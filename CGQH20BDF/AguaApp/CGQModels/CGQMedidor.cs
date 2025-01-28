using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaApp.Models
{
    public class CGQMedidor
    {
        public int idMedidor { get; set; }
        public string numeroSerie { get; set; }
        public string fechaInstalacion { get; set; }
        public string tipoMedidor { get; set; }
        public string ubicacion { get; set; }
        public string estado { get; set; }
        public string fechaUltimaRevision { get; set; }
        public int idUsuario { get; set; }
        public object idUsuarioNavigation { get; set; }
        public List<object> incidencia { get; set; }
        public List<object> lecturas { get; set; }
    }
}
