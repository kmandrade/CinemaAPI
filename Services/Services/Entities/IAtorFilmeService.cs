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
        
        Task<IEnumerable<LerAtorFilmeDto>> BuscaFilmesPorAtor(int  idAtorFilme);
        Task<Result> AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto);
        Task<Result> DeletaAtorDoFilme(int idAtor,int idFilme);
        Task<Result> AlteraAtorDoFilme(int idAtorAtual, int idFilme, int idAtorNovo);

    }
}
