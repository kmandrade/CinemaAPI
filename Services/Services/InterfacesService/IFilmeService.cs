using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.InterfacesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.InterfacesService
{
    public interface IFilmeService
    {
        Task<IEnumerable<LerFilmeDto>> BuscarTodos(int skip, int take);
        Task<IEnumerable<LerNomeFilmeDto>> BuscarFilmesMaisVotados();
        Task<Result<LerNomeFilmeDto>> BuscarPorId(int id);

        Task<LerFilmeDto> BuscarFilmeCompleto(int id);
        Task<Result> Cadastrar(CriarFilmeDto obj);
        Task<Result> Alterar(int id, AlterarFilmeDto obj);
        Task<Result> Excluir(int id);
        
        Task<Result> ArquivarFilme(int id);
        Task<Result> ReativarFilme(int id);
        Task<IEnumerable<LerFilmeDto>> BuscarFilmesArquivados(int skip, int take);
    }
}
