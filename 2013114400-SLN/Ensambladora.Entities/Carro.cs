using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensambladora.Entities
{
    public abstract class Carro
    {
        public List<Llanta> Llantas { get; set; }
        public List<Asiento> Asientos { get; set; }
        
        public Volante Volante { get; set; }
        public Parabrisas Parabrisas { get; set; }
        public Propietario Propietario { get; set; }

        public TipoCarro TipoCarro { get; set; }


        public Carro()
        {
            Llantas = new List<Llanta>();
            Asientos = new List<Asiento>();
        }

        public Carro(Volante volante, Parabrisas parabrisas, int numLlantas,
            int numAsientos, Propietario propietario, TipoCarro tipoCarro)
        {
            Llantas = new List<Llanta>(numLlantas);
            Asientos = new List<Asiento>(numAsientos);

            Volante = volante;
            Parabrisas = parabrisas;
            Propietario = propietario;

            TipoCarro = tipoCarro;
        }

        public string NumSerieChasis { get; set; }
    }
}
