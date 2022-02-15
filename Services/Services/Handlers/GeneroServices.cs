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
        IGeneroDao _generoDao;
        
        private readonly IMapper _mapper;

        public GeneroServices(IMapper mapper, IGeneroDao generoDao)
        {
            _mapper = mapper;
            
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

        public Result Altera(int id, AlterarGeneroDto obj)
        {
            var generoSelecionado = _generoDao.BuscarPorId(id);
            if (generoSelecionado == null)
            {
                return Result.Fail("Genero nao Existe");
            }
            _mapper.Map(obj,generoSelecionado);
            _generoDao.Save();
            return Result.Ok();
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
