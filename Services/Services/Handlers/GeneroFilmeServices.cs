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
        
        private readonly IMapper _mapper;

        public GeneroFilmeServices(IMapper mapper, IGeneroFilme generofilme)
        {
            _mapper = mapper;
            
            _generofilme = generofilme;
        }

        public void AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            var generoFilme = _mapper.Map<GeneroFilme>(criarGeneroFilmeDto);
            _generofilme.Incluir(generoFilme);
        }

        public IEnumerable<LerGeneroFilmeDto> BuscarFilmesPorGenero(int IdGeneroFilme)
        {
           var gf=_generofilme.BuscaFilmesPorGenero(IdGeneroFilme);
            var gfDto = _mapper.Map<IEnumerable<LerGeneroFilmeDto>>(gf);
            return gfDto;

        }

        public void DeletaGeneroDoFilme(int idGenero, int idFilme)
        {
            var selecionarGeneroDoFilme = _generofilme.BuscaGeneroDoFilme(idGenero, idFilme);
            _generofilme.Excluir(selecionarGeneroDoFilme);
        }
    }
}
