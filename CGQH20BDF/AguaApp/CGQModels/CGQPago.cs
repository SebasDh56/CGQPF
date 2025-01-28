using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaApp.Models
{
    public class CGQPago
    {
        public int idPago { get; set; }
        public int idFactura { get; set; }
        public string fechaPago { get; set; }
        public decimal montoPagado { get; set; }
        public string metodoPago { get; set; }
        public string estado { get; set; }
        public object idFacturaNavigation { get; set; }
        public List<object> recibos { get; set; }
    }
}
