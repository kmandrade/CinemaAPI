using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Ator> BuscarPorNome(string nome)
        {
            var ator = await _context.Atores
                .AsNoTracking()
                .Where(a => a.NomeAtor == nome)
                .FirstOrDefaultAsync();
            return ator;
        }
    }
}
