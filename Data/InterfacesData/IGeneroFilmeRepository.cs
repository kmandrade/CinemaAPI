using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.InterfacesData
{
    public interface IGeneroFilmeRepository : IRepository<GeneroFilme>
    {
        Task<IEnumerable<GeneroFilme>> BuscarFilmesPorGenero(int IdGeneroFilme);
        Task<GeneroFilme> BuscarGeneroDoFilme(int idGenero, int idFilme);
    }
}
