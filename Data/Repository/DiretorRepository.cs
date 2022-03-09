using Data.Context;
using Data.InterfacesData;
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

        public Task<Diretor> BuscarDiretorPorNome(string nome)
        {
            var query = _context.Diretores
                .AsNoTracking()
                .Where(d => d.NomeDiretor == nome)
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<IEnumerable<Filme>> BuscarFilmesPorDiretor(int idDiretor)
        {
            var filmes = _context.Filmes
                .AsNoTracking()
                .Include(atoresFilme => atoresFilme.AtoresFilme)
                .ThenInclude(at => at.Ator)
                .Include(generoFilme => generoFilme.GenerosFilme)
                .ThenInclude(g => g.Genero)
                .Include(d => d.Diretor)
                .Where(f => f.DiretorId == idDiretor)
                .ToListAsync();
                
            return await filmes;


        }
    }
}
