using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Entities
{
    public interface IFilmeService
    {
        Task<IEnumerable<LerFilmeDto>> BuscaTodos(int skip, int take);
        Task<LerFilmeDto> BuscaPorId(int id);

        Task<LerFilmeDto> BuscaFilmeCompleto(int id);
        Task<Result> Cadastra(CriarFilmeDto obj);
        Task<Result> Altera(int id, AlterarFilmeDto obj);
        Task<Result> Excluir(int id);

        Task<Result> ArquivarFilme(int id);
        Task<Result> ReativarFilme(int id);
        Task<IEnumerable<LerFilmeDto>> BuscaFilmesArquivados(int skip, int take);
    }
}
