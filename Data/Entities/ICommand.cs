using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface ICommand<T>
    {

        IEnumerable<T> BuscarTodos();
        T BuscarPorId(int id); 

        void Incluir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);

    }
}
