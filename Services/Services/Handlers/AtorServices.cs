using AutoMapper;
using Data.Entities;
using Domain.Dtos.AtorDto;
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
    public class AtorServices : IAtorService
    {
        
        IAtorDao _atorDao;

        private readonly IMapper _mapper;
        public AtorServices(IAtorDao atorDao, IMapper mapper)
        {
            _atorDao = atorDao;
            _mapper = mapper;
            
        }


        public async Task<LerAtorDto> ConsultaPorId(int id)
        {
            var atores = await _atorDao.BuscarPorId(id);
            if (atores == null)
            {
                return null;
            }
            var atorDto = _mapper.Map<LerAtorDto>(atores);
            return atorDto;

        }

        public async Task<IEnumerable<LerAtorDto>> ConsultaTodos(int skip, int take)
        {

            var atores = await _atorDao.BuscaTodos();
            if (atores == null)
            {
                return null;
            }
            var atoresPaginados = atores.Skip(skip).Take(take).ToList();
            
            
            var atoresDto = _mapper.Map<IEnumerable<LerAtorDto>>(atoresPaginados);
            return  atoresDto;
        }

        public async Task<Result> Cadastra(CriarAtorDto obj)
        {
            
            var atorMapeado = _mapper.Map<Ator>(obj);
            await _atorDao.Incluir(atorMapeado);
            return Result.Ok();
        }

        public async Task<Result> Altera(int id, AlterarAtorDto obj)
        {
            var atorSelecionado = await _atorDao.BuscarPorId(id);
            if (atorSelecionado == null)
            {
                return Result.Fail("Filme nao existe");
            }
            _mapper.Map(obj, atorSelecionado);
            await _atorDao.Save();
            return Result.Ok();

        }

        public async Task<Result> Excluir(int id)
        {
            var atorSelecionado = await _atorDao.BuscarPorId(id);
            if (atorSelecionado != null)
            {
                _atorDao.Excluir(atorSelecionado);
                return Result.Ok();
            }
            return Result.Fail("Esse Ator nao existe");
        }

    
    }
}
