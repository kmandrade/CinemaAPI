using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
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

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorGenero(LerGeneroDto genero)
        {
            var filmes = _filmeDao.BuscarTodos();
            var _genero = _mapper.Map<Genero>(genero);
            var queryFilmes = from filme in filmes where filme.Generos == _genero select filme;
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(queryFilmes);
            return filmesDto;
        }

        public void Cadastra(CriarGeneroDto obj)
        {
            var genero = _mapper.Map<Genero>(obj);
            _generoDao.Incluir(genero);
        }

        public void Altera(int id, AlterarGeneroDto obj)
        {
            var listaGeneros = _generoDao.BuscarTodos();
            var generoMapeado = _mapper.Map<Genero>(obj);
            var queryGeneros = from genero in listaGeneros where listaGeneros == generoMapeado select genero;
            var generoSelecionado = _mapper.Map<Genero>(queryGeneros);
            _generoDao.Alterar(generoSelecionado);
        }

        public void Excluir(int id)
        {
            var listaGeneros = _generoDao.BuscarTodos(); 
            var generoSelecionado = listaGeneros.FirstOrDefault(g => g.IdGenero == id); 
            _generoDao.Excluir(generoSelecionado);
        }
    }
}
