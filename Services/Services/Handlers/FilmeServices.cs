using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.Entities;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace Data.Services.Handlers
{
    public class FilmeServices : IFilmeService
    {

        IFilmeDao _filmeDao;
        IAtorDao _atorDao;
        IGeneroDao _generoDao;
        IDiretorDao _diretorDao;
        private readonly IMapper _mapper;

        //preciso dizer pra minha aplicação que ela deve fazer o mapeamento
        //implementar o mapeamento, interfaces e utilizar o filmedao para ter acesso ao banco pela interface
        public FilmeServices(IFilmeDao filmeDao, IMapper mapper, IGeneroDao eneroDao, IAtorDao atorDao, IDiretorDao diretorDao)
        {
            _filmeDao = filmeDao;
            _mapper = mapper;
            _generoDao = eneroDao;
            _atorDao = atorDao;
            _diretorDao = diretorDao;
        }

        public IEnumerable<LerFilmeDto> ConsultaTodos()
        {
            var listadeFilmes = _filmeDao.BuscarTodos();
            var filmesDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(listadeFilmes);
            
            return filmesDtos.OrderBy(nome => nome.Titulo);//orderby ja é um enumerable
        }

        public LerFilmeDto ConsultaPorId(int id)
        {
            var filme = _filmeDao.BuscarPorId(id);
            
            
                var filmeDto = _mapper.Map<LerFilmeDto>(filme);

            return filmeDto;
            

        }

        
        public Result Cadastra(CriarFilmeDto obj)
        {
            var filme = _filmeDao.BuscarPorNome(obj.Titulo);
            if (filme != null)
            {
                return Result.Fail("Filme ja existe");
            }
            var filmeMapeado = _mapper.Map<Filme>(obj);
            _filmeDao.Incluir(filmeMapeado);
            return Result.Ok();
        }

        public void Altera(int id, AlterarFilmeDto obj)
        {
            var filmeSelecionado = _filmeDao.BuscarPorId(id);
            if (filmeSelecionado != null)
            {
                var filmeMapeado = _mapper.Map<Filme>(obj);
                _filmeDao.Alterar(filmeMapeado);
            }

        }

        public void Excluir(int id)
        {
            var filmeSelecionado = _filmeDao.BuscarPorId(id);
            if (filmeSelecionado != null)
            {
                _filmeDao.Excluir(filmeSelecionado);
            }
        }

        public void ArquivarFilme(int id)
        {
            var filmeSelecionado = _filmeDao.BuscarPorId(id);
            
            filmeSelecionado.Situacao = SituacaoFilme.Arquivado;
            //salvar o dao _filmedao.save(); error por conta que nao tem mais

        }
    }
}
