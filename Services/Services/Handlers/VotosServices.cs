using AutoMapper;
using Data.Entities;
using Domain.Dtos.VotosDto;
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
    public class VotosServices : IVotosService
    {
        
        IVotosRepository _votosDao;
        IFilmeRepository _filmeDao;
       
        private readonly IMapper _mapper;
        public VotosServices(IVotosRepository votosDao, IMapper mapper, IFilmeRepository filmeDao )
        {
            _votosDao = votosDao;
            _mapper = mapper;
            _filmeDao = filmeDao;
            
        }

        public async Task<Result> AdicionaVotosEmFilme(AdicionaVotosDto votosDto, int idUsuario)
        {
            //busca filme pelo Dto
            var filme = await _filmeDao.BuscarPorId(votosDto.IdFilmeDto);
            
            //verifica se existe ja existe um voto desse usuario nesse filme
            var votoSelecionado = _votosDao.BuscaVotoPorFilmeEUsuario(votosDto.IdFilmeDto, idUsuario);
            
            if (votoSelecionado==null)
            {
                var voto = _mapper.Map<Votos>(votosDto);
                voto.IdUsuario = idUsuario;
                await _votosDao.Cadastra(voto);
                return Result.Ok();
            }
            return Result.Fail("eror");
            
        }

        public async Task<IEnumerable<LerVotoDto>> BuscaFilmesMaisVotados(int skip, int take)
        {
            var filmes = await _votosDao.BuscaFilmesMaisVotados();
                var votosFilmesPaginados=filmes
                .Skip(skip).Take(take).ToList();
            var votosDto = _mapper.Map<IEnumerable<LerVotoDto>>(filmes);
            return votosDto;
        }

        public async Task<Result> AlteraValorDoVotoEmFilme(int idVoto, int valorDoVoto, int idUsuario)
        {
            var votoSelecionado = await _votosDao.BuscarPorId(idVoto);

            if (votoSelecionado != null && votoSelecionado.IdUsuario==idUsuario)
            {
                votoSelecionado.ValorDoVoto = valorDoVoto;
                await _votosDao.Save();
                return Result.Ok();

            }
            return Result.Fail("error");
        }
        public async Task<Result> RemoverVoto(int idVoto, int idUsuario)
        {
            var votoSelecionado = await _votosDao.BuscarPorId(idVoto);
            if(votoSelecionado!=null && votoSelecionado.IdUsuario == idUsuario)
            {
                _votosDao.Excluir(votoSelecionado);
                return Result.Ok();

            }
            return Result.Fail("error");
            
        }

      
    }
}
