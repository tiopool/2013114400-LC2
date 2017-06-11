using _2013114400_ENT.Entities;
using _2013114400_PER.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER
{
    public class EnsambladoraDbContext:DbContext
    {

        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Automovil> Automoviles { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Cinturon> Cinturones { get; set; }
        public DbSet<Ensambladora> Ensambladoras { get; set; }
        public DbSet<Llanta> Llantas { get; set; }
        public DbSet<Parabrisas> Parabrisas { get; set; }
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Volante> Volantes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AsientoConfiguration());                   
            modelBuilder.Configurations.Add(new CarroConfiguration());
            modelBuilder.Configurations.Add(new CinturonConfiguration());
            modelBuilder.Configurations.Add(new EnsambladoraConfiguration());
            modelBuilder.Configurations.Add(new LlantaConfiguration());
            modelBuilder.Configurations.Add(new ParabrisasConfiguration());
            modelBuilder.Configurations.Add(new PropietarioConfiguration());
            modelBuilder.Configurations.Add(new VolanteConfiguration());


            Database.SetInitializer<EnsambladoraDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
