using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IGeneroService
    {
        IEnumerable<LerFilmeDto> lerFilmeDtosPorGenero(int iDgenero);

        IEnumerable<LerGeneroDto> ConsultaTodos();
        LerGeneroDto ConsultaPorId(int id);


        Result Cadastra(CriarGeneroDto obj);
        void Altera(int id, AlterarGeneroDto obj);
        void Excluir(int id);
    }
}
