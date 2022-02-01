using AutoMapper;
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

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorAtor(LerAtorDto atorDto)
        {
            var filmes = _filmeDao.BuscarTodos();
            var atorMapeado = _mapper.Map<IEnumerable<Ator>>(atorDto);
            var queryFilmes = from filme in filmes where filme.Atores == atorMapeado select filme;
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(queryFilmes);
            return filmesDto;
        }

        public void Cadastra(CriarAtorDto obj)
        {
            var ator = _mapper.Map<Ator>(obj);
            _atorDao.Incluir(ator);
        }

        public void Altera(int id, AlterarAtorDto obj)
        {
            var listaAtores = _atorDao.BuscarTodos();
            var atorSelecionado = listaAtores.FirstOrDefault(a => a.IdAtor == id);
            var atorMapeado = _mapper.Map<Ator>(obj);
            _atorDao.Alterar(atorMapeado);
        }

        public void Excluir(int id)
        {
            var listaAtores = _atorDao.BuscarTodos(); 
            var atorSelecionado = listaAtores.FirstOrDefault(a => a.IdAtor == id); 
            _atorDao.Excluir(atorSelecionado);
        }
    }
}
