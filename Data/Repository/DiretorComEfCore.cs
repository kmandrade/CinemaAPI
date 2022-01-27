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
    public class DiretorComEfCore : IDiretorDao
    {

        private readonly MyContext _context;

        public DiretorComEfCore(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Diretor> BuscarTodos()
        {
            throw new NotImplementedException();
        }
        public Diretor BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }


        public void Alterar(Diretor obj)
        {
            throw new NotImplementedException();
        }
        public void Excluir(Diretor obj)
        {
            throw new NotImplementedException();
        }

        public void Incluir(Diretor obj)
        {
            throw new NotImplementedException();
        }
    }
}
