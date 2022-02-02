using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IRepository<T>
    {

        IEnumerable<T> BuscarTodos();
        T BuscarPorNome(string nome);
        T BuscarPorId(int id); 

        void Incluir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);

    }
}
