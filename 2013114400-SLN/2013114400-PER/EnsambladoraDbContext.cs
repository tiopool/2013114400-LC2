using Ensambladora.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER
{
    public class EnsambladoraDbContext : DbContext
    {
        public DbSet<Carro> Carros { get; set; }
    }
}
