using _2013114400_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.EntityConfigurations
{
    public class AsientoConfiguration: EntityTypeConfiguration<Asiento>
    {
        public AsientoConfiguration()
        {
            ToTable("Asientos")
            .HasKey(c => c.AsientoId);

            Property(v => v.NumSerie)
                 .IsRequired()
                 .HasMaxLength(100);

            HasRequired(c => c.Cinturon)
              .WithRequiredPrincipal(c => c.Asiento);
        }

    }
}
