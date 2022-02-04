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
    public class GeneroFilmeComEfCore : BaseRepository<GeneroFilme>, IGeneroFilme
    {
        private readonly DbSet<GeneroFilme> _dbset;
        private readonly DbSet<Filme> _dbsetFilme;

        public GeneroFilmeComEfCore(MyContext _context) : base(_context)
        {
            _dbset = _context.Set<GeneroFilme>();
            _dbsetFilme = _context.Set<Filme>();
        }

        public IEnumerable<Filme> BuscaFilmesPorGenero(int IdGeneroFilme)
        {
            var filmes=_dbsetFilme
                .Include(f=>f.GenerosFilme)
                .First(g=>g.IdFilme==IdGeneroFilme);
            yield return filmes;
        }
    }
}
