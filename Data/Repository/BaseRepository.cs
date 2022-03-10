using Data.Context;
using Data.InterfacesData;
using Microsoft.EntityFrameworkCore;

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

            return await _dbSet.FindAsync(new object[] { id });
        }


        public virtual async Task<IEnumerable<T>> BuscarTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async void Excluir(T obj)
        {
            _dbSet.Remove(obj);
            await Save();

        }
        public virtual async Task Alterar(T obj)
        {
            _dbSet.Update(obj);
            await Save();
        }
        public virtual async Task Cadastrar(T obj)
        {
            _dbSet.Add(obj);
            await Save();
        }
        public async Task<int> Save()
        {

            return await _context.SaveChangesAsync();

        }

    }
}
