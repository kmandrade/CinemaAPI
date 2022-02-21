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
        
        IEnumerable<LerGeneroDto> ConsultaTodos(int skip, int take);
        LerGeneroDto ConsultaPorId(int id);


        Result Cadastra(CriarGeneroDto obj);
        Result Altera(int id, AlterarGeneroDto obj);
        void Excluir(int id);
    }
}
