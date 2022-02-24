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
    public class DiretorComEfCore : BaseRepository<Diretor>,IDiretorDao
    {
        private readonly DbSet<Diretor> _dbset;

        public DiretorComEfCore(MyContext _context):base(_context)
        {
            _dbset = _context.Set<Diretor>();

        }

        public async Task<IEnumerable<Filme>> BuscaFilmesPorDiretor(int idDiretor)
        {
            var filmes = _context.Filmes
                .AsNoTracking()
                .Include(atf => atf.AtoresFilme)
                .ThenInclude(at => at.Ator)
                .Include(gf => gf.GenerosFilme)
                .ThenInclude(g => g.Genero)
                .Include(d => d.Diretor)
                .Where(f => f.DiretorId == idDiretor)
                .ToListAsync();
                
            return await filmes;


        }
    }
}
