using Domain.Models;


namespace Data.Entities
{
    public interface IDiretorDao
    {
        public Diretor BuscaFilmePorDiretor(string nomeDiretor);
    }
}
