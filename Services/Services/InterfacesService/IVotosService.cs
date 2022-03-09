using Domain.Dtos.VotosDto;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.InterfacesService
{
    public interface IVotosService
    {
        Task<Result> AdicionarVotosEmFilme(AdicionaVotosDto adicionaVotosDto, int idUsuario);
        Task<Result> AlterarValorDoVotoEmFilme(int idFilme, int valorDoVoto, int idUsuario);
        Task<Result> ExcluirVotoDoFilme(int idFilme, int idUsuario);
    }
}
