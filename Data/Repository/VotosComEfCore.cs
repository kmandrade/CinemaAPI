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
    public class VotosComEfCore : BaseRepository<Votos>,IVotosDao

    {
        private readonly DbSet<Votos> _dbSetVotos;
      


        public VotosComEfCore(MyContext _context) : base(_context)
        {
            _dbSetVotos = _context.Set<Votos>();
 
        }

        public IEnumerable<Votos> BuscaFilmesMaisVotados()
        {
            var query = _context.Votos
                .Include(f => f.Filme);
            return query.OrderByDescending(f => f.ValorDoVoto);
        }

        public Votos BuscaVotoPorFilmeEUsuario(int idFilme, int idUsuario)
        {
            var query = _context.Votos
                .Include(f => f.Filme)
                .Include(u=>u.Usuario)
                .FirstOrDefault(v=>v.IdFilme == idFilme && v.IdUsuario==idUsuario);
            return query;
                
        }
    }
}
