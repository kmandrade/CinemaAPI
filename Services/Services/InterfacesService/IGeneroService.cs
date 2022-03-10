using Domain.Dtos.GeneroDto;
using FluentResults;

namespace Servicos.Services.InterfacesService
{
    public interface IGeneroService
    {

        Task<IEnumerable<LerGeneroDto>> BuscarTodos(int skip, int take);
        Task<Result<LerGeneroDto>> BuscarPorId(int id);


        Task<Result> Cadastrar(CriarGeneroDto obj);
        Task<Result> Alterar(int id, AlterarGeneroDto obj);
        Task<Result> Excluir(int id);
    }
}
