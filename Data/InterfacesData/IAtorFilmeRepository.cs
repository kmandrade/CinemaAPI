
using Domain.Models;

namespace Data.InterfacesData
{
    public interface IAtorFilmeRepository : IRepository<AtoresFilme>
    {
        Task<IEnumerable<AtoresFilme>> BuscarFilmesPorAtor(int IdAtor);
        Task<AtoresFilme> BuscarAtorEFilme(int idAtor, int idFilme);



    }
}
