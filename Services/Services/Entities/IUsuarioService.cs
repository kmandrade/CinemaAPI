using Domain.Dtos.UsuarioDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IUsuarioService
    {
        Task<IEnumerable<LerUsuarioDto>> BuscaTodosOsUsuarioDto(int skip, int take);
        Task<Result> CriarUsuarioNormalDto(CriarUsuarioDto criarUsuarioDto);
        Task<Result> AlteraUsuario(int idUsuario,CriarUsuarioDto criarUsuarioDto);
        Task<Result> DeletaUsuario(int idUsuario);
        Task<LerUsuarioDto> BuscaUsuarioPorId(int idUsuario);
        Task<Usuario> BuscaUsuarioPorLogin(LoginRequest loginRequest);
        Task<Result> ArquivarUsuario(int id);
        Task<Result> ReativarUsuario(int id);
        Task<IEnumerable<LerUsuarioDto>> BuscaUsuariosArquivados(int skip, int take);
    }
}
