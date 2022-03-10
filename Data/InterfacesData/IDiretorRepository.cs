using Domain.Models;

namespace Data.InterfacesData
{
    public interface IDiretorRepository : IRepository<Diretor>
    {

        Task<Diretor> BuscarDiretorPorNome(string nome);
    }
}
