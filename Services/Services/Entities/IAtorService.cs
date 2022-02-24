using Domain.Dtos.AtorDto;
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
    public interface IAtorService
    {
        

        Task<IEnumerable<LerAtorDto>> ConsultaTodos(int skip, int take);
        Task<LerAtorDto> ConsultaPorId(int id);

        Task<Result> Cadastra(CriarAtorDto obj);
        Task<Result> Altera(int id,AlterarAtorDto obj);
        Task<Result> Excluir(int id);


    }
}
