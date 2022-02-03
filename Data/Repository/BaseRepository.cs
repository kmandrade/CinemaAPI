using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly MyContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T BuscarPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> BuscarTodos()
        {
            return _dbSet.ToList();
        }

        public void Excluir(T obj)
        {
            _dbSet.Remove(obj);
            Save();

        }
        public void Alterar(T obj)
        {
            _dbSet.Update(obj);
            Save();
        }
        public void Incluir(T obj)
        {
            _dbSet.Add(obj);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
