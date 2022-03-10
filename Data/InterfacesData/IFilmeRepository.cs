using Domain.Models;


namespace Data.InterfacesData
{
    public interface IFilmeRepository : IRepository<Filme>
    {
        Task<IEnumerable<Filme>> BuscarFilmesMaisVotados();
        Task<Filme> BuscarPorNome(string nome);
        Task<Filme> BuscarPorFilmesCompletoID(int id);
        Task<IEnumerable<Filme>> BuscarFilmesArquivados();
        Task<IEnumerable<Filme>> BuscarFilmesPorDiretor(int idDiretor);
    }
}
