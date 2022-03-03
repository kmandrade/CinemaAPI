using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IDiretorRepository :IRepository<Diretor>
    {
        Task<IEnumerable<Filme>> BuscaFilmesPorDiretor(int idDiretor);
        Task<Diretor> BuscaDiretorPorNome(string nome);
    }
}
