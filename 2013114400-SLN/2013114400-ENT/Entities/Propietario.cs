using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_ENT.Entities
{
    public class Propietario
    {
        public int PropietarioId { get; set; }
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string LicenciaConducir { get; set; }


        public Carro Carro { get; set; }

    }
}
