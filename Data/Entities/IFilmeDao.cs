using Domain.Models;


namespace Data.Entities
{
    public interface IFilmeDao : IRepository<Filme>
    {
        IEnumerable<Filme> BuscaFilmesPorAtor(object ator);
        IEnumerable<Filme> BuscaFilmesPorDiretor(object diretor);

        IEnumerable<Filme> BuscaFilmesPorGenero(object genero);
    }
}
