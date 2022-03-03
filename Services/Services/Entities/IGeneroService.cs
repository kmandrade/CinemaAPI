using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IGeneroService
    {
        
        Task<IEnumerable<LerGeneroDto>> BuscaTodos(int skip, int take);
        Task<LerGeneroDto> BuscaPorId(int id);


        Task<Result> Cadastra(CriarGeneroDto obj);
        Task<Result> Altera(int id, AlterarGeneroDto obj);
        Task<Result> Excluir(int id);
    }
}
