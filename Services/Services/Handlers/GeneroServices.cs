using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
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
    public class GeneroServices : IGeneroService
    {
        IGeneroRepository _generoDao;
        
        private readonly IMapper _mapper;

        public GeneroServices(IMapper mapper, IGeneroRepository generoDao)
        {
            _mapper = mapper;
            
            _generoDao = generoDao;
        }

       

        public async Task<LerGeneroDto> ConsultaPorId(int id)
        {
            var generoId = await _generoDao.BuscarPorId(id);
            var generoDto = _mapper.Map<LerGeneroDto>(generoId);
            return generoDto;
            
        }

        public async Task<IEnumerable<LerGeneroDto>> ConsultaTodos(int skip, int take)
        {
            var listaGeneros = await _generoDao.BuscaTodos();
            if(skip <= 0 ||take <= 0)
            {
                return null;
            }
            if (take > 0)
            {
                var generosPaginados = listaGeneros.Skip(skip).Take(take).ToList();
                var listaGenerosDto = _mapper.Map<IEnumerable<LerGeneroDto>>(generosPaginados);
                return listaGenerosDto;
            }
            return null;
        }


        public async Task<Result> Cadastra(CriarGeneroDto obj)
        {
            //var genero = _generoDao.BuscarPorNome(obj.NomeGenero);
            //if (genero != null)
            //{
            //    return Result.Fail("Ator ja existe ");
            //}
            var generoMapeado = _mapper.Map<Genero>(obj);
            await _generoDao.Cadastra(generoMapeado);
            return Result.Ok();
        }

        public async Task<Result> Altera(int id, AlterarGeneroDto obj)
        {
            var generoSelecionado = await _generoDao.BuscarPorId(id);
            if (generoSelecionado == null)
            {
                return Result.Fail("Genero nao Existe");
            }
            _mapper.Map(obj,generoSelecionado);
             await _generoDao.Save();
            return Result.Ok();
        }

        public async Task<Result> Excluir(int id)
        {
            var generoSelecionado = await _generoDao.BuscarPorId(id);
            if (generoSelecionado != null)
            {
                _generoDao.Excluir(generoSelecionado);
                return Result.Ok();
            }
            return Result.Fail("error");
        }
    }
}
