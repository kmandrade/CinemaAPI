using Domain.Dtos.DiretorDto;
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
    public interface IDiretorService
    {
        Task<IEnumerable<LerFilmeDto>> lerFilmeDtosPorDiretor(int idDiretor);

        Task<IEnumerable<LerDiretorDto>> ConsultaTodos(int skip, int take);
        Task<LerDiretorDto> ConsultaPorId(int id);

        Task<Result> Cadastra(CriarDiretorDto obj);
        Task<Result> Altera(int id, AlterarDiretorDto obj);
        Task<Result> Excluir(int id);
    }
}
