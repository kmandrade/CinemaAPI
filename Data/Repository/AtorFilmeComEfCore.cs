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
    public class AtorFilmeComEfCore : BaseRepository<AtoresFilme>, IAtorFilme
    {
        private readonly DbSet<AtoresFilme> _dbset;
        private readonly DbSet<Filme> _dbsetFilme;
        public AtorFilmeComEfCore(MyContext _context):base(_context)
        {
            _dbset = _context.Set<AtoresFilme>();
            _dbsetFilme=_context.Set<Filme>();
        }

        public Filme BuscarFilmesPorAtor(int IdAtorFilme)
        {
           var filme = _dbsetFilme
                .Include(f=>f.AtoresFilme)
                .FirstOrDefault(a=>a.IdFilme==IdAtorFilme);
             return filme;

        }
    }
}
