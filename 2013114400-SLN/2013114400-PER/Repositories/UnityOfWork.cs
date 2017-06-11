using _2013114400_ENT.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly EnsambladoraDbContext _Context;
        //     private static UnityOfWork _Instance;
        //     private static readonly object _Lock = new object();

        public IAsientoRepository Asientos { get; private set; }
        public IAutomovilRepository Automoviles { get; private set; }
        public IBusRepository Buses { get; private set; }
        public ICarroRepository Carros { get; private set; }
        public ICinturonRepository Cinturones { get; private set; }
        public IEnsambladoraRepository Ensambladoras { get; private set; }
        public ILlantaRepository Llantas { get; private set; }
        public IParabrisasRepository Parabrisas { get; private set; }
        public IPropietarioRepository Propietarios { get; private set; }
        public IVolanteRepository Volantes { get; private set; }

    
        public UnityOfWork()
        {
            
            _Context = new EnsambladoraDbContext();

            Asientos = new AsientoRepository(_Context);
            Automoviles = new AutomovilRepository(_Context);
            Buses = new BusRepository(_Context);
            Carros = new CarroRepository(_Context);
            Cinturones = new CinturonRepository(_Context);
            Ensambladoras = new EnsambladoraRepository(_Context);
            Llantas = new LlantaRepository(_Context);
            Parabrisas = new ParabrisasRepository(_Context);
            Propietarios = new PropietarioRepository(_Context);
            Volantes = new VolanteRepository(_Context);


        }

        //Implementacion del patron Singleton para instanciar la clase UnityOfWork
        //Con este patron se asegura que en cualquier parte del codigo que se quiera
        //instanciar la BD, se devuelva la unica referencia.
        /*  public static UnityOfWork Instance
          {
              get
              {
                  //Variable de control para manejar el acceso concurrente
                  //al instanciamiento de las clases UnityOfWork
                  lock (_Lock)
                  {
                      if (_Instance == null)
                          _Instance = new UnityOfWork();
                  }

                  return _Instance;

              }


          }
          */

        public int SaveChanges()
        {
            return _Context.SaveChanges();
        }


        public void Dispose()
        {
            _Context.Dispose();

        }
        //metodo que guarda los cambios. lleva los cambios en memoria hacia la base de datos.
      

        //metodo que cambia el estado de una entidad en el entityframework para que luego se cambie en la base de datos
        public void StateModified(object Entity)
        {
            _Context.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
        }

    }
}
