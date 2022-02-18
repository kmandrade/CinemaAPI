using Domain.Dtos.VotosDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IVotosService
    {
        void AdicionaVotosEmFilme(AdicionaVotosDto adicionaVotosDto, int idUsuario);
        IEnumerable<LerVotoDto> BuscaFilmesMaisVotados();
        void AlteraValorDoVotoEmFilme(int idVoto, int valorDoVoto);
        void RemoverVoto(int idVoto);
    }
}
