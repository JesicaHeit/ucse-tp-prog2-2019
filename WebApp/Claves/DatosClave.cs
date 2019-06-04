using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claves
{
    public enum Roles
    {
        Padre, Directora, Docente
    }

    public class DatosClave
    {
        public string Clave { get; set; }
        public string Email { get; set; }
        public Roles[] Roles { get; set; }
    }
}
