using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wpena.lucasian.Models
{
    public class BancoCuentas
    {
        public Banco banco { get; set; }
        public Cuenta cuenta { get; set; }
        public List<Cuenta> cuentas { get; set; }
    }
}