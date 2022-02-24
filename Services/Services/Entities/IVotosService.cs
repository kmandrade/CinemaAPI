using Domain.Dtos.VotosDto;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IVotosService
    {
        Task<Result> AdicionaVotosEmFilme(AdicionaVotosDto adicionaVotosDto, int idUsuario);
        Task<IEnumerable<LerVotoDto>> BuscaFilmesMaisVotados(int skip, int take);
        Task<Result> AlteraValorDoVotoEmFilme(int idVoto, int valorDoVoto, int idUsuario);
        Task<Result> RemoverVoto(int idVoto, int idUsuario);
    }
}
