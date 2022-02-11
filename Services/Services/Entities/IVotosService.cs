using Domain.Dtos.VotosDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IVotosService
    {
        void AdicionaVotosEmFilme(AdicionaVotosDto adicionaVotosDto);
        //IEnumerable<LerVotosDto> BuscaFilmesMaisVotados();
    }
}
