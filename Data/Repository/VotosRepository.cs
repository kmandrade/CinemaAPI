using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class VotosRepository : BaseRepository<Votos>, IVotosRepository

    {
        private readonly DbSet<Votos> _dbSetVotos;



        public VotosRepository(MyContext _context) : base(_context)
        {
            _dbSetVotos = _context.Set<Votos>();

        }


        public async Task<Votos> BuscarVotoPorFilmeEUsuario(int idFilme, int idUsuario)
        {
            var query = await _context.Votos
                .Include(f => f.Filme)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(v => v.IdFilme == idFilme && v.IdUsuario == idUsuario);
            return query;

        }
        public async Task<Votos> BuscarVotoPorFilme(int idFilme)
        {
            var query = await _context.Votos
                .AsNoTracking()
                .Include(f => f.Filme)
                .Where(u => u.IdFilme == idFilme)
                .FirstOrDefaultAsync();
            return query;
        }

    }
}
