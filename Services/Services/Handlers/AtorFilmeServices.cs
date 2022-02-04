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

        public LerFilmeDto BuscaFilmesPorAtor(int idAtorFilme)
        {
            Filme filmes = _atorfilme.BuscarFilmesPorAtor(idAtorFilme);//aqui ja tenho os filmes
            var filmesMapeados = _mapper.Map<LerFilmeDto>(filmes);
            return filmesMapeados;
        }
        /*
        public IEnumerable<LerFilmeDto> BuscaFilmesPorAtor(int idAtorFilme)
        {
            var filmes = _filmeDao.BuscarPorId(idAtorFilme);
            var filmesDto = _mapper.Map<LerFilmeDto>(filmes);
            if (filmes == null)
            {
                yield return null;
            }
            yield return filmesDto;
        }
        */
        public IEnumerable<LerAtorFilmeDto> BuscaTodosAtoresFilmes()
        {
            var atoresFilmes = _atorfilme.BuscarTodos();
            var dtoAt = _mapper.Map<IEnumerable<LerAtorFilmeDto>>(atoresFilmes);
            return dtoAt;

        }
    }
}
