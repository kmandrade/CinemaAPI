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
       
        private readonly IMapper mapper;
        public VotosServices(IVotosDao votosDao, IMapper mapper, IFilmeDao filmeDao )
        {
            _votosDao = votosDao;
            this.mapper = mapper;
            _filmeDao = filmeDao;
         
            
        }

        public void AdicionaVotosEmFilme(AdicionaVotosDto votosDto, int idUsuario)
        {
            var filme = _filmeDao.BuscarPorId(votosDto.IdFilmeDto);
 
            if(filme!=null)
            {
                var votos = mapper.Map<Votos>(votosDto);
                votos.IdUsuario = idUsuario;
                _votosDao.Incluir(votos);
            }
        }

        public IEnumerable<LerVotoDto> BuscaFilmesMaisVotados()
        {
            var filmes=_votosDao.BuscaFilmesMaisVotados();
            var votosDto = mapper.Map<IEnumerable<LerVotoDto>>(filmes);
            return votosDto;
        }
    }
}
