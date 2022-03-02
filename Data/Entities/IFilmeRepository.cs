using Domain.Models;


namespace Data.Entities
{
    public interface IFilmeRepository : IRepository<Filme>
    {
        //IEnumerable<Filme> BuscaFilmesMaisVotados(Votos votos);
       
       Task<Filme> BuscarPorNome(string nome);
       Task<Filme> BuscarPorFilmesCompletoID(int id);
        
    }
}
