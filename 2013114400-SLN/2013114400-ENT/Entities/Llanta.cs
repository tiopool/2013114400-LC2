using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_ENT.Entities
{
    public class Llanta
    {
        public int LlantaId { get; set; }
        public string NumSerie { get; set; }



        public object Carro { get; set; }

        public Carro Carros { get; set; }
        public int CarroId { get; set; }

    }
}
