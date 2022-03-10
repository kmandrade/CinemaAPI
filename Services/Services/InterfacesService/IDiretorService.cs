using Domain.Dtos.DiretorDto;
using FluentResults;

namespace Servicos.Services.InterfacesService
{
    public interface IDiretorService
    {


        Task<IEnumerable<LerDiretorDto>> BuscarTodos(int skip, int take);
        Task<Result<LerDiretorDto>> BuscarPorId(int id);

        Task<Result> Cadastrar(CriarDiretorDto obj);
        Task<Result> Alterar(int id, AlterarDiretorDto obj);
        Task<Result> Excluir(int id);
    }
}
