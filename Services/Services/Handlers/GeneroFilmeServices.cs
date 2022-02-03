using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.FilmeGenero;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class GeneroFilmeServices:IGeneroFilmeService
    {
        IGeneroFilme _generofilme;
        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;

        public GeneroFilmeServices(IMapper mapper, IFilmeDao filmeDao, IGeneroFilme generofilme)
        {
            _mapper = mapper;
            _filmeDao = filmeDao;
            _generofilme = generofilme;
        }

        public void AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            var generoFilme = _mapper.Map<GeneroFilme>(criarGeneroFilmeDto);
            _generofilme.Incluir(generoFilme);
        }

        public IEnumerable<LerFilmeDto> BuscarFilmesPorGenero(LerGeneroFilmeDto lerGeneroFilmeDto)
        {
            var generoFilme = _mapper.Map<GeneroFilme>(lerGeneroFilmeDto);
            return (IEnumerable<LerFilmeDto>)_generofilme.BuscaFilmesPorGenero(generoFilme);
        }
    }
}
