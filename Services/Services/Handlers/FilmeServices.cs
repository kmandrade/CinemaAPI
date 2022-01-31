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
    public class FilmeServices : IFilmeService
    {

        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;
        //preciso dizer pra minha aplicação que ela deve fazer o mapeamento
        //implementar o mapeamento, interfaces e utilizar o filmedao para ter acesso ao banco pela interface
        public FilmeServices(IFilmeDao filmeDao, IMapper mapper)
        {
            _filmeDao = filmeDao;
            _mapper = mapper;
        }

        public IEnumerable<LerFilmeDto> ConsultaTodos()
        {
            var listadeFilmes = _filmeDao.BuscarTodos();
            var filmesDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(listadeFilmes);
            return filmesDtos.OrderBy(nome => nome.Titulo);
        }

        public LerFilmeDto ConsultaPorId(int id)
        {
            var filme = _filmeDao.BuscarPorId(id);
            var filmeDto = _mapper.Map<LerFilmeDto>(filme);
            return filmeDto;
        }


        public void Cadastra(CriarFilmeDto obj)
        {
            var filmeMapeado = _mapper.Map<Filme>(obj);
            _filmeDao.Incluir(filmeMapeado);
        }

        public void Altera(int id,AlterarFilmeDto obj)
        {
            var listaFilmes = _filmeDao.BuscarTodos(); 
            var filmeSelecionado = listaFilmes.FirstOrDefault(f => f.IdFilme == id);
            var filmeMapeado = _mapper.Map<Filme>(obj);
            _filmeDao.Alterar(filmeMapeado);
        }

        public void Excluir(int id)
        {
            var listaFilmes = _filmeDao.BuscarTodos();
            var filmeSelecionado = listaFilmes.FirstOrDefault(f => f.IdFilme == id); 
            _filmeDao.Excluir(filmeSelecionado);
        }
    }
}
