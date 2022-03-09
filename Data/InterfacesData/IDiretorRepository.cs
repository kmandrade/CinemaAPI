using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.InterfacesData
{
    public interface IDiretorRepository :IRepository<Diretor>
    {
        Task<IEnumerable<Filme>> BuscarFilmesPorDiretor(int idDiretor);
        Task<Diretor> BuscarDiretorPorNome(string nome);
    }
}
