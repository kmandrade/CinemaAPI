﻿using AutoMapper;
using Data.Entities;
using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class AtorServices : IAtorService
    {
        IFilmeDao _filmeDao;
        IAtorDao _atorDao;
        private readonly IMapper _mapper;
        public AtorServices(IAtorDao atorDao, IMapper mapper, IFilmeDao filmeDao)
        {
            _atorDao = atorDao;
            _mapper = mapper;
            _filmeDao = filmeDao;
        }


        public LerAtorDto ConsultaPorId(int id)
        {
            var atores = _atorDao.BuscarPorId(id);
            var atorDto = _mapper.Map<LerAtorDto>(atores);
            return atorDto;
        }

        public IEnumerable<LerAtorDto> ConsultaTodos()
        {
            var atores = _atorDao.BuscarTodos();
            var atoresDto = _mapper.Map<IEnumerable<LerAtorDto>>(atores);
            return atoresDto;
        }

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorAtor(LerAtorDto ator)
        {
            var filmes = _filmeDao.BuscarTodos();
            var atores = _mapper.Map<Ator>(ator);
            var queryFilmes = from filme in filmes where filme.Atores == ator select filme;
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(queryFilmes);
            return filmesDto;
        }

        public void Cadastra(CriarAtorDto obj)
        {
            var ator = _mapper.Map<Ator>(obj);
            _atorDao.Incluir(ator);
        }

        public void Altera(AlterarAtorDto obj)
        {
            var listaAtores = _atorDao.BuscarTodos();
            var atorMapeado = _mapper.Map<Ator>(obj);
            var queryAtores = from ator in listaAtores where listaAtores == atorMapeado select ator;
            var atorSelecionado = _mapper.Map<Ator>(queryAtores);
            _atorDao.Alterar(atorSelecionado);
        }

        public void Remove(LerAtorDto obj)
        {
            var listaAtores = _atorDao.BuscarTodos();
            var atorMapeado = _mapper.Map<Ator>(obj);
            var queryAtores = from ator in listaAtores where listaAtores == atorMapeado select ator;
            var atorSelecionado = _mapper.Map<Ator>(queryAtores);
            _atorDao.Excluir(atorSelecionado);
        }
    }
}