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

        public IEnumerable<GeneroFilme> BuscaFilmesPorGenero(int IdGeneroFilme)
        {
            var queryFilmes = _context.GenerosFilmes
            .Include(g => g.Genero)
            .Include(f => f.Filme)
            .ThenInclude(d => d.Diretor)
            .Where(gf => gf.IdGenero == IdGeneroFilme).ToList();
            return queryFilmes;
        }

        public GeneroFilme BuscaGeneroDoFilme(int idGenero)
        {
            var query =_context.GenerosFilmes
                .Include(f => f.Filme)
                .Include(g=>g.Genero)
                .FirstOrDefault(gf=>gf.IdGenero==idGenero);
            //pegar o primeiro GeneroFilme
            //que possua IdGenero da Entidade igual do parametro
            return query;
        }
    }
}
