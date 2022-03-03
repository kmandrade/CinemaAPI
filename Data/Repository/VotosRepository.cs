﻿using Data.Context;
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
    public class VotosRepository : BaseRepository<Votos>,IVotosRepository

    {
        private readonly DbSet<Votos> _dbSetVotos;
      


        public VotosRepository(MyContext _context) : base(_context)
        {
            _dbSetVotos = _context.Set<Votos>();
 
        }

        public async Task<IQueryable<Votos>> BuscaFilmesMaisVotados()
        {
            var query = _context.Votos
                .Include(f => f.Filme);
               
            return query.OrderByDescending(f => f.ValorDoVoto);
        }

        public async Task<Votos> BuscaVotoPorFilmeEUsuario(int idFilme, int idUsuario)
        {
            var query = await _context.Votos
                .Include(f => f.Filme)
                .Include(u=>u.Usuario)
                .FirstOrDefaultAsync(v=>v.IdFilme == idFilme && v.IdUsuario==idUsuario);
            return query;
                
        }
    }
}