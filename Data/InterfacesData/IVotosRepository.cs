using Domain.Models;

namespace Data.InterfacesData
{
    public interface IVotosRepository : IRepository<Votos>
    {
        Task<Votos> BuscarVotoPorFilme(int idFilme);
        Task<Votos> BuscarVotoPorFilmeEUsuario(int idFilme, int idUsuario);

    }
}
