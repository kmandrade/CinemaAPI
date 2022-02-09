using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IGeneroFilme : IRepository<GeneroFilme>
    {
        IEnumerable<GeneroFilme> BuscaFilmesPorGenero(int IdGeneroFilme);
        GeneroFilme BuscaGeneroDoFilme(int idGenero);
    }
}
