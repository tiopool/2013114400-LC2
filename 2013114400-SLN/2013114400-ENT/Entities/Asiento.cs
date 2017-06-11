using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_ENT.Entities
{
    public class Asiento
    {
        public int AsientoId { get; set; }      
        public string NumSerie { get; set; }


        public Cinturon Cinturon { get; set; }
        public Carro Carro { get; set; }

        public int CarroId { get; set; }



    }
}
