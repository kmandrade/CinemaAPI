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
    public class DiretorComEfCore : IDiretorDao
    {

        private readonly MyContext _context;

        public DiretorComEfCore(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Diretor> BuscarTodos()
        {
            var diretores = _context.Diretores;
            return diretores;
        }
        public Diretor BuscarPorId(int id)
        {
            return _context.Diretores.Find(id);
        }


        public void Alterar(Diretor obj)
        {
            _context.Diretores.Update(obj);
            _context.SaveChanges();
        }

        public void Excluir(Diretor obj)
        {
            _context.Diretores.Remove(obj);
            _context.SaveChanges();
        }

        public void Incluir(Diretor obj)
        {
            _context.Diretores.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Filme> BuscaFilmesPorDiretor(Diretor diretor)
        {
            yield return _context.Filmes
               .Include(a => a.Diretor)
               .First(f => f.Diretor == diretor);
        }
    }
}
