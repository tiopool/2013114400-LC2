using _2013114400_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.EntityConfigurations
{
    public class EnsambladoraConfiguration : EntityTypeConfiguration<Ensambladora>
    {
        public EnsambladoraConfiguration()
        {
            ToTable("Ensambladoras")
                .HasKey(c => c.EnsambladoraId);

            HasMany(c => c.Carros)
                .WithRequired(d => d.Ensambladora)
                .HasForeignKey(c => c.EnsambladoraId);
        }

    }
}
