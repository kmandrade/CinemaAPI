using Domain.Models;

namespace Data.InterfacesData
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> BuscarUsuarioPorNomeESenha(string nome, string password);
        Task<Usuario> BuscarUsuarioPorLogin(string nome);
    }
}
