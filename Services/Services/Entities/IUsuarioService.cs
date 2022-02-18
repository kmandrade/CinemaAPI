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
        IEnumerable<LerUsuarioDto> BuscaTodosOsUsuarioDto();
        void CriarUsuarioNormalDto(CriarUsuarioDto criarUsuarioDto);
        void AlteraUsuario(int idUsuario,CriarUsuarioDto criarUsuarioDto);
        void DeletaUsuario(int idUsuario);
        LerUsuarioDto BuscaUsuarioPorId(int idUsuario);

        public Usuario BuscaUsuarioPorLogin(LoginRequest loginRequest);
    }
}
