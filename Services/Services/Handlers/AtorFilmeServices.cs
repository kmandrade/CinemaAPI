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
        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;

        public AtorFilmeServices(IMapper mapper, IFilmeDao filmeDao, IAtorFilme atorfilme)
        {
            _mapper = mapper;
            _filmeDao = filmeDao;
            _atorfilme = atorfilme;
        }

        public void AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            var atorFilme = _mapper.Map<AtoresFilme>(criarAtorFilmeDto);
            _atorfilme.Incluir(atorFilme);
            
        }

        public IEnumerable<LerAtorFilmeDto> BuscaFilmesPorAtor(int idAtorFilme)
        {
            var atf = _atorfilme.BuscarFilmesPorAtor(idAtorFilme);
            var atfDto = _mapper.Map<IEnumerable<LerAtorFilmeDto>>(atf);
            return atfDto;
        }
        
        public IEnumerable<LerAtorFilmeDto> BuscaTodosAtoresFilmes()
        {
            var atoresFilmes = _atorfilme.BuscarTodos();
            var dtoAt = _mapper.Map<IEnumerable<LerAtorFilmeDto>>(atoresFilmes);
            return dtoAt;

        }
    }
}
