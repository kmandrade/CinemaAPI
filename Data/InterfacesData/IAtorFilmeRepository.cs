
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.InterfacesData
{
    public interface IAtorFilmeRepository : IRepository<AtoresFilme>
    {
       Task<IEnumerable<AtoresFilme>> BuscarFilmesPorAtor(int IdAtor);
       Task<AtoresFilme> BuscarAtorEFilme(int idAtor, int idFilme);

        

    }
}
