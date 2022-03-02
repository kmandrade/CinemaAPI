using Data.Context;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AtorRepository : BaseRepository<Ator>, IAtorRepository
    {
        
        private readonly DbSet<Ator> _dbSetAtor;
        private readonly DbSet<AtoresFilme> _dbSetAtoresFilme;
        public AtorRepository(MyContext _context) : base(_context)
        {
            _dbSetAtor = _context.Set<Ator>();
            _dbSetAtoresFilme = _context.Set<AtoresFilme>();
           
        }

    }
}
