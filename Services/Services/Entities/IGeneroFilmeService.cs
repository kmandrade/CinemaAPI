using Domain.Dtos.FilmeDto;
using Domain.Dtos.FilmeGenero;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IGeneroFilmeService
    {
        Task<Result> AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto);
        Task<IEnumerable<LerGeneroFilmeDto>> BuscarFilmesPorGenero(int IdGeneroFilme);
        Task<Result> DeletaGeneroDoFilme(int idGenero, int idFilme);
        Task<Result> AlteraGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo);


    }
}
