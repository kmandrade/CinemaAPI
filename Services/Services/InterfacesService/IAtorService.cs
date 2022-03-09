using Domain.Dtos.AtorDto;
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
    public interface IAtorService
    {
        

        Task<IEnumerable<LerAtorDto>> BuscarTodos(int skip, int take);
        Task<Result<LerAtorDto>> BuscarPorId(int id);

        Task<Result> Cadastrarr(CriarAtorDto obj);
        Task<Result> Alterar(int id,AlterarAtorDto obj);
        Task<Result> Excluir(int id);


    }
}
