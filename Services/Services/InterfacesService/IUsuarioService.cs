using Domain.Dtos.UsuarioDto;
using Domain.Models;
using FluentResults;

namespace Servicos.Services.InterfacesService
{
    public interface IUsuarioService
    {
        Task<IEnumerable<LerUsuarioDto>> BuscarTodosOsUsuarioDto(int skip, int take);
        Task<Result> CriarUsuarioNormalDto(CriarUsuarioDto criarUsuarioDto);
        Task<Result<LerUsuarioDto>> BuscarUsuarioPorId(int idUsuario);
        Task<Usuario> BuscarUsuarioPorLogin(string nome);
        Task<IEnumerable<LerUsuarioDto>> BuscarUsuariosArquivados(int skip, int take);
        Task<Result> AlterarUsuario(int idUsuario, CriarUsuarioDto criarUsuarioDto);
        Task<Result> ExcluirUsuario(int idUsuario);


        Task<Result> ArquivarUsuario(int id);
        Task<Result> ReativarUsuario(int id);

        Task<Result> CriarUsuarioHash(CriarUsuarioDto criarUsuarioDto);
        Task<bool> ValidaSenhaAsync(LoginRequest login);


    }
}
