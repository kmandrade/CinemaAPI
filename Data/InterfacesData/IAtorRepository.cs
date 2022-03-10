using Domain.Models;

namespace Data.InterfacesData
{
    public interface IAtorRepository : IRepository<Ator>
    {
        Task<Ator> BuscarPorNome(string nome);
    }
}
