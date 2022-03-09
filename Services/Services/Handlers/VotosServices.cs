using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.VotosDto;
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
    public class VotosServices : IVotosService
    {

        private readonly IVotosRepository _votosRepository;
        private readonly IFilmeRepository _filmeRepository;
       
        private readonly IMapper _mapper;
        public VotosServices(IVotosRepository votosRepository, IMapper mapper, IFilmeRepository filmeRepository )
        {
            _votosRepository = votosRepository;
            _mapper = mapper;
            _filmeRepository = filmeRepository;
            
        }

        public async Task<Result> AdicionarVotosEmFilme(AdicionaVotosDto adicionaVotosDto, int idUsuario)
        {
            //busca filme pelo Dto
            var filme = await _filmeRepository.BuscarPorId(adicionaVotosDto.IdFilmeDto);
            if(filme == null)
            {
                return Result.Fail("Filme nao existe");
            }
            //verifica se existe ja existe um voto desse usuario nesse filme
            var votoSelecionado = await _votosRepository.BuscarVotoPorFilmeEUsuario(adicionaVotosDto.IdFilmeDto, idUsuario);
            
            if (votoSelecionado==null)
            {
                var voto = _mapper.Map<Votos>(adicionaVotosDto);
                voto.IdUsuario = idUsuario;
                await _votosRepository.Cadastrar(voto);
                filme.TotalDeVotos += adicionaVotosDto.ValorDoVotoDto;
                await _filmeRepository.Save();
                return Result.Ok();
            }
            return Result.Fail("error");
            
        }

       

        public async Task<Result> AlterarValorDoVotoEmFilme(int idFilme, int valorDoVoto, int idUsuario)
        {
            var votoSelecionado = await _votosRepository.BuscarVotoPorFilme(idFilme);
            if (votoSelecionado == null)
            {
                return Result.Fail("Filme nao encontrado");
            }
            if (votoSelecionado.IdUsuario!=idUsuario)
            {
                return Result.Fail("Usuario nao votou neste filme");

            }
            votoSelecionado.ValorDoVoto = valorDoVoto;
            await _votosRepository.Save();
            return Result.Ok();

        }
        public async Task<Result> ExcluirVotoDoFilme(int idFilme, int idUsuario)
        {
            var votoSelecionado = await _votosRepository.BuscarVotoPorFilme(idFilme);
            if (votoSelecionado == null)
            {
                return Result.Fail("Filme nao encontrado");
            }
            if( votoSelecionado.IdUsuario != idUsuario)
            {

                return Result.Fail("Usuario nao votou neste filme");
            }
            _votosRepository.Excluir(votoSelecionado);
            return Result.Ok();
            
            
        }

      
    }
}
