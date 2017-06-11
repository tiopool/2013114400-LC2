using _2013114400_ENT.Entities;
using _2013114400_ENT.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013114400_PER.Repositories
{
    public class EnsambladoraRepository : Repository<Ensambladora>, IEnsambladoraRepository
    {
        public EnsambladoraRepository(EnsambladoraDbContext context) : base(context)
        {
           
        }

    }
}
