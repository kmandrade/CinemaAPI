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
    public class FilmeRepository : BaseRepository<Filme>, IFilmeRepository 
    {

        private readonly DbSet<Filme> _dbset;

        public FilmeRepository(MyContext _context) : base(_context)
        {
            _dbset = _context.Set<Filme>();
        }
        public async Task<Filme> BuscarPorNome(string nome)
        {
            var _filme = await _context.Filmes
                .Where(f => f.Titulo == nome)
                .FirstOrDefaultAsync();

            return _filme;
        }
        public async Task<Filme> BuscarPorFilmesCompletoID(int id)
        {
            var filme = await _context.Filmes
                .Include(d => d.Diretor)
                .Include(atf=>atf.AtoresFilme)
                .ThenInclude(a=>a.Ator)
                .Include(gf=>gf.GenerosFilme)
                .ThenInclude(g=>g.Genero)
                .FirstOrDefaultAsync(f => f.IdFilme == id);
            
            return filme;
               
        }


    }
}
