using Domain.Models;

namespace Data.InterfacesData
{
    public interface IGeneroRepository : IRepository<Genero>
    {
        Task<Genero> BuscarPorNome(string nome);
    }
}
