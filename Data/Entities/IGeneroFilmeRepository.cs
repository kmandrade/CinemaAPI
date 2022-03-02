using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IGeneroFilmeRepository : IRepository<GeneroFilme>
    {
        Task<IEnumerable<GeneroFilme>> BuscaFilmesPorGenero(int IdGeneroFilme);
        Task<GeneroFilme> BuscaGeneroDoFilme(int idGenero, int idFilme);
    }
}
