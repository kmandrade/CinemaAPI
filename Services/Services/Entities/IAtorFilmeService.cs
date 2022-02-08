using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
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
        IEnumerable<AtoresFilme> BuscaFilmesPorAtor(int  idAtorFilme);
        IEnumerable<LerAtorFilmeDto> BuscaTodosAtoresFilmes();
    }
}
