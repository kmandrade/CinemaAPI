using AutoMapper;
using Data.Entities;
using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
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

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorAtor(int  iDAtor)
        {
            var filmes = _filmeDao.BuscarPorId(iDAtor);

            var filmesDto = _mapper.Map<LerFilmeDto>(filmes);
            yield return filmesDto;
        }

        public Result Cadastra(CriarAtorDto obj)
        {
            //var ator = _atorDao.BuscarPorNome(obj.NomeAtor);
            //if(ator != null)
            //{
            //    return Result.Fail("Ator ja existe ");
            //}
            var atorMapeado = _mapper.Map<Ator>(obj);
            _atorDao.Incluir(atorMapeado);
            return Result.Ok();
        }

        public void Altera(int id, AlterarAtorDto obj)
        {
            var atorSelecionado = _atorDao.BuscarPorId(id);
            if (atorSelecionado != null)
            {
                var atorMapeado = _mapper.Map<Ator>(obj);
                _atorDao.Alterar(atorMapeado);
            }
            
        }

        public void Excluir(int id)
        {
            var atorSelecionado = _atorDao.BuscarPorId(id);
            if (atorSelecionado != null)
            {
                _atorDao.Excluir(atorSelecionado);
            }
        }
    }
}
