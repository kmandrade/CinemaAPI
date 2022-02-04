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
    public class GeneroFilmeComEfCore : BaseRepository<GeneroFilme>, IGeneroFilme
    {
        private readonly DbSet<GeneroFilme> _dbset;

        public GeneroFilmeComEfCore(MyContext _context) : base(_context)
        {
            _dbset = _context.Set<GeneroFilme>();
        }

        public IEnumerable<Filme> BuscaFilmesPorGenero(GeneroFilme generoFilme)
        {
            var filme = _context
                .Filmes
                .Include(a => a.GenerosFilme)
                .ThenInclude(gf => gf.Filme)
                .FirstOrDefault();
            if (filme != null)
            {
                foreach (var item in filme.GenerosFilme)
                {
                    return (IEnumerable<Filme>)item.Genero;
                }
            }
            return null;
        }
    }
}
