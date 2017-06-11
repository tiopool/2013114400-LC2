using _2013114400_ENT.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_ENT.Entities
{
    public class Bus : Carro
    {
        public int BusId { get; set; }
       


        public TipoBus TipoBus { get; set; }

        public Bus(Volante volante, Parabrisas parabrisas, int numLlantas,
                         int numAsientos, Propietario propietario, TipoCarro tipoCarro, TipoBus tipoBus)
            :base(volante, parabrisas, numLlantas, numAsientos, propietario, tipoCarro)
        {
            TipoBus = tipoBus;
        }

        public Bus()
        {

        }

    }
}
