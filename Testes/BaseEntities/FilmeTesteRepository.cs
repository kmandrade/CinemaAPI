using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.BaseEntities
{
    public static class FilmeTesteRepository
    {
        public static List<CriarFilmeDto> RetornaCriarFilmeDto(List<CriarFilmeDto> filme)
        {
            var filmes = new List<CriarFilmeDto>()
            {
                new CriarFilmeDto() { Titulo = "Filme1", DiretorId = 1, Duracao =100, Situacao=SituacaoEntities.Ativado},
                new CriarFilmeDto() { Titulo = "Filme2", DiretorId = 1, Duracao =100, Situacao=SituacaoEntities.Arquivado}
                
            };
            foreach(var f in filmes)
            {
                return filmes;
            }
            return null;
        }



    }
}
