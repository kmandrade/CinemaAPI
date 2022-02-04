using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IAtorFilmeService
    {
        void AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto);
        IEnumerable<LerFilmeDto> BuscaFilmesPorAtor(int  idAtorFilme);
        IEnumerable<LerAtorFilmeDto> BuscaTodosAtoresFilmes();
    }
}
