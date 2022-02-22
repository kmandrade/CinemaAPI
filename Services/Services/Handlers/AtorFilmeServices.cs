using AutoMapper;
using Data.Entities;
using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Handlers
{
    public class AtorFilmeServices:IAtorFilmeService
    {
        IAtorFilme _atorfilme;
        
        private readonly IMapper _mapper;

        public AtorFilmeServices(IMapper mapper, IAtorFilme atorfilme)
        {
            _mapper = mapper;
            
            _atorfilme = atorfilme;
        }

        

        public IEnumerable<LerAtorFilmeDto> BuscaFilmesPorAtor(int idAtorFilme)
        {
            var atf = _atorfilme.BuscarFilmesPorAtor(idAtorFilme);
            if (atf != null)
            {
                var atfDto = _mapper.Map<IEnumerable<LerAtorFilmeDto>>(atf);
                return atfDto;
            }
            return null;
        }

        public Result AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            var atorFilme = _mapper.Map<AtoresFilme>(criarAtorFilmeDto);
            if(atorFilme != null)
            {
                _atorfilme.Incluir(atorFilme);
                return Result.Ok();   
            }
            return Result.Fail(errorMessage: "Ator ou Filme nao existem");
            
        }
        public Result AlteraAtorDoFilme(int idAtorAtual, int idFilme, int idAtorNovo)
        {
            var AtorFilmeSelecionado = _atorfilme.BuscaAtorDoFilme(idAtorAtual, idFilme);
            if(AtorFilmeSelecionado != null)
            {
                AtorFilmeSelecionado.IdAtor = idAtorNovo;
                _atorfilme.Save();
                return Result.Ok();
            }
            return Result.Fail(errorMessage: "Dados nao Conferem");
        }

        public Result DeletaAtorDoFilme(int idAtor,int idFilme)
        {
            var selecionarAtorDoFilme = _atorfilme.BuscaAtorDoFilme(idAtor,idFilme);
            if (selecionarAtorDoFilme != null)
            {
                _atorfilme.Excluir(selecionarAtorDoFilme);
                return Result.Ok();
            }
            return Result.Fail(errorMessage: "Dados nao Conferem");

        }

       
    }
}
