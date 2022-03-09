using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.DiretorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.InterfacesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Handlers
{
    public class DiretorServices : IDiretorService
    {
        private readonly IDiretorRepository _diretorRepository;
        private readonly IFilmeRepository _filmeRepository;
        private readonly IMapper _mapper;
        public DiretorServices(IDiretorRepository diretorRepository, IMapper mapper , IFilmeRepository filmeRepository)
        {
            _mapper=mapper;
            _diretorRepository=diretorRepository;
            _filmeRepository=filmeRepository;

        }

        public async Task<IEnumerable<LerFilmeDto>> lerFilmeDtosPorDiretor(int idDiretor)
        {

            var filmes = await _diretorRepository.BuscarFilmesPorDiretor(idDiretor);
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmes);
            return filmesDto;
           
        }

        public async Task<IEnumerable<LerDiretorDto>> ConsultarTodos(int skip, int take)
        {
            var diretores = await _diretorRepository.BuscarTodos();
            var diretoresPaginados = diretores.Skip(skip).Take(take).ToList();
            var diretoresMapeados = _mapper.Map<IEnumerable<LerDiretorDto>>(diretoresPaginados);
            return diretoresMapeados;
        }

        public async Task<Result<LerDiretorDto>> ConsultarPorId(int id)
        {
            var diretor = await _diretorRepository.BuscarPorId(id);
            if (diretor == null)
            {
                return Result.Fail("Diretor nao encontrado");
            }

            var diretorDto = _mapper.Map<LerDiretorDto>(diretor);//converte pra dto e manda pra tela
            return Result.Ok(diretorDto);
        }

        public async Task<Result> Cadastrar(CriarDiretorDto obj)
        {
            var diretor = _mapper.Map<Diretor>(obj);
           await _diretorRepository.Cadastrar(diretor);
            return Result.Ok();
        }

        public async Task<Result> Alterar(int id, AlterarDiretorDto diretorDto)
        {
            var diretorSelecionado = _diretorRepository.BuscarPorId(id);
            if(diretorSelecionado != null)
            {
                var diretorMapeado = _mapper.Map<Diretor>(diretorDto);
               await _diretorRepository.Alterar(diretorMapeado);
                return Result.Ok();
            }
            return Result.Fail("errror");
            
        }

        public async Task<Result> Excluir(int id)
        {
            var diretorSelecionado = await _diretorRepository.BuscarPorId(id);
            if (diretorSelecionado != null)
            {
                _diretorRepository.Excluir(diretorSelecionado);
                return Result.Ok();
            }
            return Result.Fail("fail");
        }

       
    }
}
