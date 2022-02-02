using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IAtorService
    {
        IEnumerable<LerFilmeDto> lerFilmeDtosPorAtor(int  idAtor);

        IEnumerable<LerAtorDto> ConsultaTodos();
        LerAtorDto ConsultaPorId(int id);


        Result Cadastra(CriarAtorDto obj);
        void Altera(int id,AlterarAtorDto obj);
        void Excluir(int id);


    }
}
