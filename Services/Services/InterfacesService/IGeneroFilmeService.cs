using Domain.Dtos.FilmeDto;
using Domain.Dtos.FilmeGenero;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.InterfacesService
{
    public interface IGeneroFilmeService
    {
        Task<Result> AdicionarGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto);
        Task<IEnumerable<LerGeneroFilmeDto>> BuscarFilmesPorGenero(int IdGeneroFilme);
        Task<Result> DeletarGeneroDoFilme(int idGenero, int idFilme);
        Task<Result> AlterarGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo);


    }
}
