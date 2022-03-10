using Domain.Dtos.FilmeDto;
using FluentResults;

namespace Domain.Services.InterfacesService
{
    public interface IFilmeService
    {
        Task<IEnumerable<LerFilmeDto>> BuscarTodos(int skip, int take);
        Task<IEnumerable<LerNomeFilmeDto>> BuscarFilmesMaisVotados();
        Task<Result<LerNomeFilmeDto>> BuscarPorId(int id);
        Task<IEnumerable<LerNomeFilmeDto>> BuscarFilmesPorDiretor(int idDiretor);
        Task<Result<LerFilmeDto>> BuscarFilmeCompleto(int id);
        Task<Result> Cadastrar(CriarFilmeDto obj);
        Task<Result> Alterar(int id, AlterarFilmeDto obj);
        Task<Result> Excluir(int id);

        Task<Result> ArquivarFilme(int id);
        Task<Result> ReativarFilme(int id);
        Task<IEnumerable<LerFilmeDto>> BuscarFilmesArquivados(int skip, int take);
    }
}
