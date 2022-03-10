using Domain.Models;

namespace Servicos.Services.InterfacesService
{
    public interface ITokenService
    {
        public string GenerateToken(Usuario user);

    }
}
