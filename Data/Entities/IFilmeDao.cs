using Domain.Models;


namespace Data.Entities
{
    public interface IFilmeDao : IRepository<Filme>
    {
        //IEnumerable<Filme> BuscaFilmesMaisVotados(Votos votos);
       
        Filme BuscarPorNome(string nome);
        Filme BuscarPorFilmesCompletoID(int id);
        
    }
}
