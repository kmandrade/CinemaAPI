using Data.Context;
using Data.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class VotosComEfCore : IVotosDao
    {
        private readonly MyContext _context;

        public VotosComEfCore(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Filme> BuscaFilmesMaisVotados()
        {
            throw new NotImplementedException();
        }

        public void VotarEmFilme(int idFilme, int ValorDoVoto)
        {
            throw new NotImplementedException();
        }
    }
}
