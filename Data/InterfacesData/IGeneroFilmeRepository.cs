using Domain.Models;

namespace Data.InterfacesData
{
    public interface IGeneroFilmeRepository : IRepository<GeneroFilme>
    {
        Task<IEnumerable<GeneroFilme>> BuscarFilmesPorGenero(int IdGeneroFilme);
        Task<GeneroFilme> BuscarGeneroDoFilme(int idGenero, int idFilme);
    }
}
