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
    public class FilmeComEfCore : BaseRepository<Filme>, IFilmeDao 
    {

        private readonly DbSet<Filme> _dbset;

        public FilmeComEfCore(MyContext _context) : base(_context)
        {
            _dbset = _context.Set<Filme>();
        }
        public Filme BuscarPorNome(string nome)
        {
            var _filme = _context.Filmes.Where(f => f.Titulo == nome);
            return _filme.FirstOrDefault();
        }
        public Filme BuscarPorFilmesCompletoID(int id)
        {
            var filme = _dbset
                .Include(d=>d.Diretor)
                .Include(a=>a.AtoresFilme)
                .ThenInclude(x=>x.Ator)
                .Include(gf=>gf.GenerosFilme)
                .ThenInclude(g=>g.Genero)
                .FirstOrDefault(f=>f.IdFilme == id);
            return filme;
          
            
        }

    }
}
