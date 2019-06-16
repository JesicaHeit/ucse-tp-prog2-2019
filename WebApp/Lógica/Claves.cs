using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lógica
{
    public enum Roless
    {
        Padre, Directora, Docente, Nulo
    }

    public class Claves
    {
        public int ID { get; set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public Roless[] Roles { get; set; }
    }
}
