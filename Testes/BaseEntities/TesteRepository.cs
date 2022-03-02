using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.BaseEntities
{
    public static class TesteRepository
    {
        public static List<CriarFilmeDto> RetornaCriarFilmeDto(List<CriarFilmeDto> filme)
        {
            var filmes = new List<CriarFilmeDto>()
            {
                new CriarFilmeDto() { Titulo = "Filme1", DiretorId = 1, Duracao =100},
                new CriarFilmeDto() { Titulo = "Filme2", DiretorId = 1, Duracao =100}
                
            };
            foreach(var f in filmes)
            {
                return filmes;
            }
            return null;
        }

        public static bool Retorna_True_OU_False_Result(Result resultado)
        {
            if (resultado.IsFailed)
            {
                return false;
            }
            return true;
        }

    }
}
