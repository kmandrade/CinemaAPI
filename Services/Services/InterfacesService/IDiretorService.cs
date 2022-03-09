using Domain.Dtos.DiretorDto;
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
    public interface IDiretorService
    {
        Task<IEnumerable<LerFilmeDto>> lerFilmeDtosPorDiretor(int idDiretor);

        Task<IEnumerable<LerDiretorDto>> ConsultarTodos(int skip, int take);
        Task<Result<LerDiretorDto>> ConsultarPorId(int id);

        Task<Result> Cadastrar(CriarDiretorDto obj);
        Task<Result> Alterar(int id, AlterarDiretorDto obj);
        Task<Result> Excluir(int id);
    }
}
