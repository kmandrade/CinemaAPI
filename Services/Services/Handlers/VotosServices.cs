using AutoMapper;
using Data.Entities;
using Domain.Dtos.VotosDto;
using Domain.Models;
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
        
        IVotosDao _votosDao;
        IFilmeDao _filmeDao;
       
        private readonly IMapper _mapper;
        public VotosServices(IVotosDao votosDao, IMapper mapper, IFilmeDao filmeDao )
        {
            _votosDao = votosDao;
            _mapper = mapper;
            _filmeDao = filmeDao;
            
        }

        public void AdicionaVotosEmFilme(AdicionaVotosDto votosDto, int idUsuario)
        {
            var filme = _filmeDao.BuscarPorId(votosDto.IdFilmeDto);
 
            if(filme!=null)
            {
                var votos = _mapper.Map<Votos>(votosDto);
                votos.IdUsuario = idUsuario;
                _votosDao.Incluir(votos);
            }
        }

        public IEnumerable<LerVotoDto> BuscaFilmesMaisVotados()
        {
            var filmes=_votosDao.BuscaFilmesMaisVotados();
            var votosDto = _mapper.Map<IEnumerable<LerVotoDto>>(filmes);
            return votosDto;
        }

        public void AlteraValorDoVotoEmFilme(int idVoto, int valorDoVoto)
        {
            var votoSelecionado = _votosDao.BuscarPorId(idVoto);
            votoSelecionado.ValorDoVoto=valorDoVoto;
            _votosDao.Save();
        }
        public void RemoverVoto(int idVoto)
        {
            var votoSelecionado = _votosDao.BuscarPorId(idVoto);
            _votosDao.Excluir(votoSelecionado);
        }

      
    }
}
