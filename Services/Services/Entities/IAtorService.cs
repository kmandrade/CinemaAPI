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
        

        IEnumerable<LerAtorDto> ConsultaTodos(int skip, int take);
        LerAtorDto ConsultaPorId(int id);

        Result Cadastra(CriarAtorDto obj);
        Result Altera(int id,AlterarAtorDto obj);
        void Excluir(int id);


    }
}
