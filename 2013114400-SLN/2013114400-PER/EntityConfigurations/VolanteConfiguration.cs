using _2013114400_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.EntityConfigurations
{
    public class VolanteConfiguration : EntityTypeConfiguration<Volante>
    {

        public VolanteConfiguration()
        {
            ToTable("Volante")
               .HasKey(c => c.VolanteId);
            Property(v => v.NumSerie)
                 .IsRequired()
                 .HasMaxLength(100);

        }

    }
}
