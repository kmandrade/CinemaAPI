using Data.Context;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AtorFilmeRepository : BaseRepository<AtoresFilme>, IAtorFilmeRepository
    {
        private readonly DbSet<AtoresFilme> _dbsetAtorFilme;
        
        
        public AtorFilmeRepository(MyContext _context):base(_context)
        {
            _dbsetAtorFilme = _context.Set<AtoresFilme>();
            
        }

        public  async Task<IEnumerable<AtoresFilme>> BuscarFilmesPorAtor(int IdAtorFilme)
        {
            //sem conseguir acessar genero
            var queryFilmes = _context.AtoresFilmes
                .Include(a => a.Ator)
                .Include(f => f.Filme)
                .ThenInclude(d=>d.Diretor)
                .AsNoTracking()
                .Where(atf => atf.IdAtor == IdAtorFilme)
                .ToListAsync();
            return await queryFilmes;
         
        }
        public async Task<AtoresFilme> BuscaAtorDoFilme(int idAtor, int idFilme)
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
