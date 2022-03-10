using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class GeneroRepository : BaseRepository<Genero>, IGeneroRepository
    {
        private readonly DbSet<Genero> _dbset;

        public GeneroRepository(MyContext _context) : base(_context)
        {
            _dbset = _context.Set<Genero>();
        }

        public Task<Genero> BuscarPorNome(string nome)
        {
            var query = _context.Generos
                .AsNoTracking()
                .Where(g => g.NomeGenero == nome)
                .FirstOrDefaultAsync();
            return query;
        }
    }
}
