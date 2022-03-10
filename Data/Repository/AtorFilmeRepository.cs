using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AtorFilmeRepository : BaseRepository<AtoresFilme>, IAtorFilmeRepository
    {
        private readonly DbSet<AtoresFilme> _dbsetAtorFilme;


        public AtorFilmeRepository(MyContext _context) : base(_context)
        {
            _dbsetAtorFilme = _context.Set<AtoresFilme>();

        }

        public async Task<IEnumerable<AtoresFilme>> BuscarFilmesPorAtor(int IdAtor)
        {
            //sem conseguir acessar genero
            var queryFilmes = _context.AtoresFilmes
                .Include(a => a.Ator)
                .Include(f => f.Filme)
                .ThenInclude(d => d.Diretor)
                .AsNoTracking()
                .Where(atoresFilme => atoresFilme.IdAtor == IdAtor)
                .ToListAsync();
            return await queryFilmes;

        }
        public async Task<AtoresFilme> BuscarAtorEFilme(int idAtor, int idFilme)
        {

            var selecionaAtorFilme = _context.AtoresFilmes
            .Include(a => a.Ator)
            .Include(f => f.Filme)
            .AsNoTracking()
            .Where(at => at.IdAtor == idAtor && at.IdFilme == idFilme)
            .FirstAsync();

            return await selecionaAtorFilme;


        }
    }
}
