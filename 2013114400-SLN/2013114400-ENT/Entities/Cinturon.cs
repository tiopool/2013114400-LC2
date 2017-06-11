using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_ENT.Entities
{
    public class Cinturon
    {

        public int CinturonId { get; set; }
        public string NumSerie { get; set; }
        public int Metraje { get; set; }


        public Asiento Asiento { get; set; }


    }
}
