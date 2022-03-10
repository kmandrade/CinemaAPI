using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class DiretorRepository : BaseRepository<Diretor>, IDiretorRepository
    {
        private readonly DbSet<Diretor> _dbset;

        public DiretorRepository(MyContext _context) : base(_context)
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


    }
}
