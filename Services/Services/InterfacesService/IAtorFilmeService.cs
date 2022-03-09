using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.InterfacesService
{
    public interface IAtorFilmeService
    {
        
        Task<IEnumerable<LerAtorFilmeDto>> BuscarFilmesPorAtor(int  idAtorFilme);
        Task<Result> AdicionarAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto);
        Task<Result> DeletarAtorDoFilme(int idAtor,int idFilme);
        Task<Result> AlterarAtorDoFilme(int idAtorAtual, int idFilme, int idAtorNovo);

    }
}
