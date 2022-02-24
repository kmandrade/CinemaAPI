using Data.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IAtorFilme : IRepository<AtoresFilme>
    {
       Task<IEnumerable<AtoresFilme>> BuscarFilmesPorAtor(int IdAtorFilme);
       Task<AtoresFilme> BuscaAtorDoFilme(int idAtor, int idFilme);

        

    }
}
