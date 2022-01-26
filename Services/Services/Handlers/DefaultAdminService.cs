using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Handlers
{
    public class DefaultAdminService : IAdminService
    {

        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;
        //preciso dizer pra minha aplicação que ela deve fazer o mapeamento
        //implementar o mapeamento, interfaces e utilizar o filmedao para ter acesso ao banco pela interface
        public DefaultAdminService(IFilmeDao filmeDao, IMapper mapper)
        {
            _filmeDao = filmeDao;
            _mapper = mapper;
        }

        public void CadastraFilme(CriarFilmeDto filme)
        {
            var filmeMapeado = _mapper.Map<Filme>(filme);
            _filmeDao.Incluir(filmeMapeado);
        }

        public IEnumerable<LerFilmeDto> ConsultaFilmes()
        {
            var filmes = _filmeDao.BuscarTodos();
            var filmeDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(filmes);
            return filmeDtos;
        }

        public LerFilmeDto ConsultaFilmePorId(int id)
        {
            throw new NotImplementedException();
        }

        public void ModificaFilme(AlterarFilmeDto filme)
        {
            throw new NotImplementedException();
        }

        public void RemoveFilme(Filme filme)
        {
            throw new NotImplementedException();
        }
    }
}
