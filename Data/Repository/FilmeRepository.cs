using Data.Context;
using Data.InterfacesData;
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
            var _filme = await _context.Filmes.AsNoTracking()
                .Where(f => f.Titulo == nome)
                .FirstOrDefaultAsync();

            return _filme;
        }
        
        public async Task<Filme> BuscarPorFilmesCompletoID(int id)
        {
            var filme = await _context.Filmes
                .Include(d => d.Diretor)
                .Include(atoresFilme=>atoresFilme.AtoresFilme)
                .ThenInclude(a=>a.Ator)
                .Include(generoFilme=>generoFilme.GenerosFilme)
                .ThenInclude(g=>g.Genero)
                .FirstOrDefaultAsync(f => f.IdFilme == id);
            
            return filme;
               
        }
        public override async Task<IEnumerable<Filme>> BuscarTodos()
        {
            var query = await _context.Filmes.AsNoTracking()
                .Where(f => f.Situacao == SituacaoEntities.Ativado)
                .OrderByDescending(f => f.Titulo).ToListAsync();

            return query;
        }
        public async Task<IEnumerable<Filme>> BuscarFilmesMaisVotados()
        {
            var query = await _context.Filmes.AsNoTracking()
                .Include(f => f.Votos)
                .Where(f=>f.Situacao==SituacaoEntities.Ativado)
                .OrderByDescending(f => f.TotalDeVotos).ToListAsync();

            return query;

        }

        public async Task<IEnumerable<Filme>> BuscarFilmesArquivados()
        {
            var query = await  _context.Filmes.AsNoTracking()
                .Where(f => f.Situacao==SituacaoEntities.Arquivado)
                .OrderByDescending(f => f.TotalDeVotos).ToListAsync();

            return query;
        }
    }
}
