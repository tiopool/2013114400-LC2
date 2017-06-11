using _2013114400_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.EntityConfigurations
{
    public class CarroConfiguration : EntityTypeConfiguration<Carro>
    {
        public CarroConfiguration()
        {
            ToTable("Carro")
               .HasKey(c => c.CarroId);

            Property(v => v.NumSerieChasis)
                 .IsRequired()
                 .HasMaxLength(100);

            Property(v => v.NumSerieMotor)
                 .IsRequired()
                 .HasMaxLength(100);

            //Relaciones

            HasMany(c => c.Asientos)
                .WithRequired(d => d.Carro)
                .HasForeignKey(c => c.CarroId);


            HasMany(c => c.Llantas)
                .WithRequired(d => d.Carros)
                .HasForeignKey(c => c.CarroId);

            HasRequired(c => c.Volante)
                .WithRequiredPrincipal(d => d.Carro);

            HasOptional(c => c._Parabrisas)
                 .WithOptionalPrincipal(d => d.Carro);

              Map<Automovil>(m => m.Requires("Discriminator").HasValue("Automovil"));
              Map<Bus>(m => m.Requires("Discriminator").HasValue("Bus"));


        }
    }
}