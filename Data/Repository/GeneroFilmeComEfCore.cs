using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class GeneroFilmeComEfCore : BaseRepository<GeneroFilme>, IGeneroFilmeRepository
    {
        private readonly DbSet<GeneroFilme> _dbset;


        public GeneroFilmeComEfCore(MyContext _context) : base(_context)
        {
            _dbset = _context.Set<GeneroFilme>();

        }

        public async Task<IEnumerable<GeneroFilme>> BuscarFilmesPorGenero(int IdGeneroFilme)
        {
            var queryFilmes = await _context.GenerosFilmes
            .Include(g => g.Genero)
            .Include(f => f.Filme)
            .ThenInclude(d => d.Diretor)
            .Where(generoFilme => generoFilme.IdGenero == IdGeneroFilme)
            .ToListAsync();

            return queryFilmes;
        }

        public async Task<GeneroFilme> BuscarGeneroDoFilme(int idGenero, int idFilme)
        {
            var query = await _context.GenerosFilmes
                .Include(g => g.Genero)
                .Include(f => f.Filme)
                .Where(generoFilme => generoFilme.IdGenero == idGenero && generoFilme.IdFilme == idFilme)
                .FirstAsync();
            return query;
        }
    }
}
