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
    public class DiretorRepository : BaseRepository<Diretor>,IDiretorRepository
    {
        private readonly DbSet<Diretor> _dbset;

        public DiretorRepository(MyContext _context):base(_context)
        {
            _dbset = _context.Set<Diretor>();

        }

        public Task<Diretor> BuscaDiretorPorNome(string nome)
        {
            var query = _context.Diretores
                .AsNoTracking()
                .Where(d => d.NomeDiretor == nome)
                .FirstOrDefaultAsync();
            return query;
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
