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
    public class AtorComEfCore : IAtorDao
    {


        private readonly MyContext _context;

        public AtorComEfCore(MyContext context)
        {
            _context = context;
        }

        public Ator BuscarPorId(int id)
        {
           return _context.Atores.Find(id);
        }
        public Ator BuscarPorNome(string nome)
        {
            var _ator = _context.Atores.Where(a => a.NomeAtor == nome);
            return _ator.FirstOrDefault();
        }

        public IEnumerable<Ator> BuscarTodos()
        {
            var atores = _context.Atores;
             return atores;
        }


        public void Excluir(Ator obj)
        {
            _context.Atores.Remove(obj);
            _context.SaveChanges();
        }

        public void Alterar(Ator obj)
        {
            _context.Atores.Update(obj);
            _context.SaveChanges();
        }

        public void Incluir(Ator obj)
        {
            _context.Atores.Add(obj);
            _context.SaveChanges();
        }
    }
}
