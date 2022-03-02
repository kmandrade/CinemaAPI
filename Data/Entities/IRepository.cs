using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IRepository<T>
    {

        Task<IEnumerable<T>> BuscaTodos();
        
        Task<T> BuscarPorId(int id); 

        Task Cadastra(T obj);
        Task Alterar(T obj);
        void Excluir(T obj);
        Task<int> Save();


    }
}
