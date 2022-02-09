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
        IEnumerable<AtoresFilme> BuscarFilmesPorAtor(int IdAtorFilme);
        AtoresFilme BuscaAtorDoFilme(int idAtor);
    }
}
