using Domain.Dtos.UsuarioDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IUsuarioService
    {
        IEnumerable<LerUsuarioDto> LerTodosOsUsuarioDto();
        void CriarUsuarioDto(CriarUsuarioDto criarUsuarioDto);
        Usuario BuscaUsuarioPorLogin(LoginRequest loginRequest);
    }
}
