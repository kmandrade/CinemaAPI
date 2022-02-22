using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IAtorFilmeService
    {
        
        IEnumerable<LerAtorFilmeDto> BuscaFilmesPorAtor(int  idAtorFilme);
        Result AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto);
        Result DeletaAtorDoFilme(int idAtor,int idFilme);
        Result AlteraAtorDoFilme(int idAtorAtual, int idFilme, int idAtorNovo);

    }
}
