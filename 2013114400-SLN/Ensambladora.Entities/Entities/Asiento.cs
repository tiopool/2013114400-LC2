using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensambladora.Entities
{
    public class Asiento
    {
        public Cinturon Cinturon { get; set; }
        public string NumSerie { get; set; }

        public Asiento()
        {
            Cinturon = new Cinturon();
        }
        
    }
}
