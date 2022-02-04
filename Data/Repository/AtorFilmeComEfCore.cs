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
    public class AtorFilmeComEfCore : BaseRepository<AtoresFilme>, IAtorFilme
    {
        private readonly DbSet<AtoresFilme> _dbset;

        public AtorFilmeComEfCore(MyContext _context):base(_context)
        {
            _dbset = _context.Set<AtoresFilme>();
        }

        public IEnumerable<Filme> BuscarFilmesPorAtor(AtoresFilme atorfilme)
        {
            



                //lendo filmes por ator
                var filme = _context
                    .Filmes //contexto de filmes 
                    .Include(a => a.AtoresFilme)//seleciona chave ator
                    .ThenInclude(at => at.Filme)
                    .FirstOrDefault();
                if (filme != null)
                {
                    foreach (var item in filme.AtoresFilme)
                    {
                        return (IEnumerable<Filme>)item.Ator;
                    }
                }
                return null;

            
        }
    }
}
