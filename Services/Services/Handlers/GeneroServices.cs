using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using FluentResults;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class GeneroServices : IGeneroService
    {
        IGeneroDao _generoDao;
        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;

        public GeneroServices(IMapper mapper, IFilmeDao filmeDao, IGeneroDao generoDao)
        {
            _mapper = mapper;
            _filmeDao = filmeDao;
            _generoDao = generoDao;
        }

       

        public LerGeneroDto ConsultaPorId(int id)
        {
            var generoId = _generoDao.BuscarPorId(id);
            var generoDto = _mapper.Map<LerGeneroDto>(generoId);
            return generoDto;
            
        }

        public IEnumerable<LerGeneroDto> ConsultaTodos()
        {
            var listaGeneros = _generoDao.BuscarTodos();
            var listaGenerosDto = _mapper.Map<IEnumerable<LerGeneroDto>>(listaGeneros);
            return listaGenerosDto;
        }

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorGenero(int iDgenero)
        {
            var filmes = _filmeDao.BuscarPorId(iDgenero);

            var filmesDto = _mapper.Map<LerFilmeDto>(filmes);
            yield return filmesDto;
        }

        public Result Cadastra(CriarGeneroDto obj)
        {
            //var genero = _generoDao.BuscarPorNome(obj.NomeGenero);
            //if (genero != null)
            //{
            //    return Result.Fail("Ator ja existe ");
            //}
            var generoMapeado = _mapper.Map<Genero>(obj);
            _generoDao.Incluir(generoMapeado);
            return Result.Ok();
        }

        public void Altera(int id, AlterarGeneroDto obj)
        {
            var generoSelecionado = _generoDao.BuscarPorId(id);
            if (generoSelecionado != null)
            {
                var generoMapeado = _mapper.Map<Genero>(obj);
                _generoDao.Alterar(generoMapeado);
            }
        }

        public void Excluir(int id)
        {
            var generoSelecionado = _generoDao.BuscarPorId(id);
            if (generoSelecionado != null)
            {
                _generoDao.Excluir(generoSelecionado);
            }
        }
    }
}
