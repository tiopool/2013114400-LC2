using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_ENT.IRepositories
{
    public interface IUnityOfWork : IDisposable
    {

        //Cada una de las propiedades debe ser de solo lectura

        IAsientoRepository Asientos { get; }
        IAutomovilRepository Automoviles { get; }
        IBusRepository Buses { get; }
        ICarroRepository Carros { get; }
        ICinturonRepository Cinturones { get; }
        IEnsambladoraRepository Ensambladoras { get; }
        ILlantaRepository Llantas { get; }
        IParabrisasRepository Parabrisas { get; }
        IPropietarioRepository Propietarios { get; }
        IVolanteRepository Volantes { get; }


        //Metodo que guardara los cambios de la base de datos.
        int SaveChanges();

        void StateModified(object entity);

    }
}
