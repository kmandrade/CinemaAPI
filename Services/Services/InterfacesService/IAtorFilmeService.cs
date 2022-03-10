using Domain.Dtos.AtorFilme;
using FluentResults;

namespace Servicos.Services.InterfacesService
{
    public interface IAtorFilmeService
    {

        Task<IEnumerable<LerAtorFilmeDto>> BuscarFilmesPorAtor(int idAtorFilme);
        Task<Result> AdicionarAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto);
        Task<Result> DeletarAtorDoFilme(int idAtor, int idFilme);
        Task<Result> AlterarAtorDoFilme(int idAtorAtual, int idFilme, int idAtorNovo);

    }
}
