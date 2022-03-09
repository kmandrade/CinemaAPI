using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.InterfacesService
{
    public interface IGeneroService
    {
        
        Task<IEnumerable<LerGeneroDto>> BuscarTodos(int skip, int take);
        Task<Result<LerGeneroDto>> BuscarPorId(int id);


        Task<Result> Cadastrar(CriarGeneroDto obj);
        Task<Result> Alterar(int id, AlterarGeneroDto obj);
        Task<Result> Excluir(int id);
    }
}
