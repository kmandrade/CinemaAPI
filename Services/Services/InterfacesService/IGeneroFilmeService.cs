using Domain.Dtos.FilmeGenero;
using FluentResults;

namespace Servicos.Services.InterfacesService
{
    public interface IGeneroFilmeService
    {
        Task<Result> AdicionarGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto);
        Task<IEnumerable<LerGeneroFilmeDto>> BuscarFilmesPorGenero(int IdGeneroFilme);
        Task<Result> DeletarGeneroDoFilme(int idGenero, int idFilme);
        Task<Result> AlterarGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo);


    }
}
