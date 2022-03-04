﻿using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.FilmeGenero;
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
    public class GeneroFilmeServices:IGeneroFilmeService
    {
        IGeneroFilmeRepository _generofilme;
        IGeneroRepository _generoRepository;
        IFilmeRepository _filmeRepository;
        private readonly IMapper _mapper;

        public GeneroFilmeServices(IMapper mapper, IGeneroFilmeRepository generofilme, IGeneroRepository generoRepository, IFilmeRepository filmeRepository)
        {
            _mapper = mapper;

            _generofilme = generofilme;
            _generoRepository = generoRepository;
            _filmeRepository = filmeRepository;
        }
        public async Task<IEnumerable<LerGeneroFilmeDto>> BuscaFilmesPorGenero(int IdGeneroFilme)
        {
            
            var gf = await _generofilme.BuscaFilmesPorGenero(IdGeneroFilme);
            if (gf == null)
            {
                return null;
            }
            var gfDto = _mapper.Map<IEnumerable<LerGeneroFilmeDto>>(gf);
            return gfDto;

        }
        public async Task<Result> AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            var generoFilme = _mapper.Map<GeneroFilme>(criarGeneroFilmeDto);
            await _generofilme.Cadastra(generoFilme);
            return Result.Ok();
        }

        public async Task<Result> AlteraGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo)
        {
            var GeneroFilmeSelecionado = await _generofilme.BuscaGeneroDoFilme(idGeneroAntigo, idFilme);
            GeneroFilmeSelecionado.IdGenero = iDGeneroNovo;
            await _generofilme.Save();
            return Result.Ok();
        }

        public async Task<Result> DeletaGeneroDoFilme(int idGenero, int idFilme)
        {
            var selecionarGeneroDoFilme = await _generofilme.BuscaGeneroDoFilme(idGenero, idFilme);
            _generofilme.Excluir(selecionarGeneroDoFilme);
            return Result.Ok();
        }

       
    }
}
