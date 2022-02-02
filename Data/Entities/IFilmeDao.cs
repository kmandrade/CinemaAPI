using Domain.Models;


namespace Data.Entities
{
    public interface IFilmeDao : IRepository<Filme>
    {
        //IEnumerable<Filme> BuscaFilmesMaisVotados(Votos votos);
        void Save();
        Filme BuscarPorNome(string nome);

    }
}
