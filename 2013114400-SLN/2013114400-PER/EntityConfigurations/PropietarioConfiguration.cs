using _2013114400_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.EntityConfigurations
{
    public class PropietarioConfiguration : EntityTypeConfiguration<Propietario>
    {
        public PropietarioConfiguration()
        {
            ToTable("Propietario")
               .HasKey(c => c.PropietarioId);

            Property(v => v.Nombres)
                 .IsRequired()
                 .HasMaxLength(100);

            Property(v => v.Apellidos)
                 .IsRequired()
                 .HasMaxLength(100);

            Property(v => v.DNI)
                 .IsRequired()
                 .HasMaxLength(100);

            Property(v => v.LicenciaConducir)
                 .IsRequired()
                 .HasMaxLength(100);


            HasRequired(c => c.Carro)
             .WithRequiredPrincipal(d => d.Propietario);
        }
    }
}
