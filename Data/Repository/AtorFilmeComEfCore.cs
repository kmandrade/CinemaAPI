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

        public  IEnumerable<AtoresFilme> BuscarFilmesPorAtor(int IdAtorFilme)
        {
            var filmes = _context.Filmes
                 .Include(at => at.AtoresFilme)
                 .ThenInclude(a => a.Ator)
                   .FirstOrDefault();

            foreach (var item in filmes.AtoresFilme)
            {
                yield return item;
            }
        }
    }
}
