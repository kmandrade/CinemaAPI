using Domain.Dtos.AtorDto;
using FluentResults;

namespace Servicos.Services.InterfacesService
{
    public interface IAtorService
    {


        Task<IEnumerable<LerAtorDto>> BuscarTodos(int skip, int take);
        Task<Result<LerAtorDto>> BuscarPorId(int id);

        Task<Result> Cadastrarr(CriarAtorDto obj);
        Task<Result> Alterar(int id, AlterarAtorDto obj);
        Task<Result> Excluir(int id);


    }
}
