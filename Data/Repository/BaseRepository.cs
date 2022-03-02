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

        public async Task<T> BuscarPorId(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return  await _dbSet.FindAsync();
        }
        
        public async Task<IEnumerable<T>> BuscaTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual void Excluir(T obj)
        {
            _dbSet.Remove(obj);
             Save();

        }
        public virtual async Task Alterar(T obj)
        {
            _dbSet.Update(obj);
            await Save();
        }
        public virtual async Task Cadastra(T obj)
        {
            _dbSet.Add(obj);
            await Save();
        }
        public async Task<int> Save()
        {
            
           return await _context.SaveChangesAsync();
            
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
