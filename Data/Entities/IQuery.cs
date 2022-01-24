using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IQuery<T>
    {
        T BuscarPorNomeDiretorOuGeneroOuAutor(object NomeDiretorOuAutorOuGenero);
    }
}
