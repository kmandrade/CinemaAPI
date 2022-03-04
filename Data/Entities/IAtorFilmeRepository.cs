using Data.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IAtorFilmeRepository : IRepository<AtoresFilme>
    {
       Task<IEnumerable<AtoresFilme>> BuscaFilmesPorAtor(int IdAtor);
       Task<AtoresFilme> BuscaAtorEFilme(int idAtor, int idFilme);

        

    }
}
