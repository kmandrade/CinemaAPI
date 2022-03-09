using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
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
    public class GeneroServices : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        
        private readonly IMapper _mapper;

        public GeneroServices(IMapper mapper, IGeneroRepository generoRepository)
        {
            _mapper = mapper;
            
            _generoRepository = generoRepository;
        }

       

        public async Task<Result<LerGeneroDto>> BuscarPorId(int id)
        {
            var generoId = await _generoRepository.BuscarPorId(id);
            if(generoId == null)
            {
                return Result.Fail("Genero nao encontrado");
            }
            var generoDto = _mapper.Map<LerGeneroDto>(generoId);
            return  Result.Ok(generoDto);
            
        }

        public async Task<IEnumerable<LerGeneroDto>> BuscarTodos(int skip, int take)
        {
            var listaGeneros = await _generoRepository.BuscarTodos();
            
            if (listaGeneros == null)
            {
                return null;
            }
                var generosPaginados = listaGeneros.Skip(skip).Take(take).ToList();
                var listaGenerosDto = _mapper.Map<IEnumerable<LerGeneroDto>>(generosPaginados);
                return listaGenerosDto;
            
            
        }


        public async Task<Result> Cadastrar(CriarGeneroDto obj)
        {
            var genero = await _generoRepository.BuscarPorNome(obj.NomeGenero);
            if (genero != null)
            {
                return Result.Fail("Genero ja existe ");
            }
            var generoMapeado = _mapper.Map<Genero>(obj);
            await _generoRepository.Cadastrar(generoMapeado);
            return Result.Ok();
        }

        public async Task<Result> Alterar(int id, AlterarGeneroDto obj)
        {
            var generoSelecionado = await _generoRepository.BuscarPorId(id);
            if (generoSelecionado == null)
            {
                return Result.Fail("Genero nao Existe");
            }
            _mapper.Map(obj,generoSelecionado);
             await _generoRepository.Save();
            return Result.Ok();
        }

        public async Task<Result> Excluir(int id)
        {
            var generoSelecionado = await _generoRepository.BuscarPorId(id);
            if (generoSelecionado != null)
            {
                _generoRepository.Excluir(generoSelecionado);
                return Result.Ok();
            }
            return Result.Fail("error");
        }
    }
}
