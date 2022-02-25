using AutoMapper;
using Data.Entities;
using Domain.Dtos.DiretorDto;
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
    public class DiretorServices : IDiretorService
    {
        IDiretorDao _diretorDao;
        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;
        public DiretorServices(IDiretorDao diretorDao, IMapper mapper , IFilmeDao filmeDao)
        {
            _mapper=mapper;
            _diretorDao=diretorDao;
            _filmeDao=filmeDao;

        }

        public async Task<IEnumerable<LerFilmeDto>> lerFilmeDtosPorDiretor(int idDiretor)
        {

            var filmes = await _diretorDao.BuscaFilmesPorDiretor(idDiretor);
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmes);
            return filmesDto;
           
        }

        public async Task<IEnumerable<LerDiretorDto>> ConsultaTodos(int skip, int take)
        {
            var diretores = await _diretorDao.BuscaTodos();
            var diretoresPaginados = diretores.Skip(skip).Take(take).ToList();
            var diretoresMapeados = _mapper.Map<IEnumerable<LerDiretorDto>>(diretoresPaginados);
            return diretoresMapeados;
        }

        public async Task<LerDiretorDto> ConsultaPorId(int id)
        {
            var diretor = await _diretorDao.BuscarPorId(id);//vai pegar so o numero do id e verifica no banco
            var diretorDto = _mapper.Map<LerDiretorDto>(diretor);//converte pra dto e manda pra tela
            return diretorDto;
        }

        public async Task<Result> Cadastra(CriarDiretorDto obj)
        {
            var diretor = _mapper.Map<Diretor>(obj);
           await _diretorDao.Incluir(diretor);
            return Result.Ok();
        }

        public async Task<Result> Altera(int id, AlterarDiretorDto diretorDto)
        {
            var diretorSelecionado = _diretorDao.BuscarPorId(id);
            if(diretorSelecionado != null)
            {
                var diretorMapeado = _mapper.Map<Diretor>(diretorDto);
               await _diretorDao.Alterar(diretorMapeado);
                return Result.Ok();
            }
            return Result.Fail("errror");
            
        }

        public async Task<Result> Excluir(int id)
        {
            var diretorSelecionado = await _diretorDao.BuscarPorId(id);
            if (diretorSelecionado != null)
            {
                _diretorDao.Excluir(diretorSelecionado);
                return Result.Ok();
            }
            return Result.Fail("fail");
        }

       
    }
}
