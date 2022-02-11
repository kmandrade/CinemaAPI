using AutoMapper;
using Data.Entities;
using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class AtorFilmeServices:IAtorFilmeService
    {
        IAtorFilme _atorfilme;
        
        private readonly IMapper _mapper;

        public AtorFilmeServices(IMapper mapper, IAtorFilme atorfilme)
        {
            _mapper = mapper;
            
            _atorfilme = atorfilme;
        }

        

        public IEnumerable<LerAtorFilmeDto> BuscaFilmesPorAtor(int idAtorFilme)
        {
            var atf = _atorfilme.BuscarFilmesPorAtor(idAtorFilme);
            var atfDto = _mapper.Map<IEnumerable<LerAtorFilmeDto>>(atf);
            return atfDto;
        }

        public void AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            var atorFilme = _mapper.Map<AtoresFilme>(criarAtorFilmeDto);
            _atorfilme.Incluir(atorFilme);

        }
        public void DeletaAtorDoFilme(int idAtor,int idFilme)
        {
            var selecionarAtorDoFilme = _atorfilme.BuscaAtorDoFilme(idAtor,idFilme);
            _atorfilme.Excluir(selecionarAtorDoFilme);
        }

        
    }
}
