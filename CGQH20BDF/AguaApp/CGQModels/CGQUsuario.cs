using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaApp.Models
{
    public class CGQUsuario
    {
        
            public int idUsuario { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string direccion { get; set; }
            public string telefono { get; set; }
            public string correo { get; set; }
            public string tipoUsuario { get; set; }
            public string estadoServicio { get; set; }
            public string fechaRegistro { get; set; }
            public List<object> facturas { get; set; }
            public List<object> incidencia { get; set; }
            public List<object> medidors { get; set; }
    }
}
