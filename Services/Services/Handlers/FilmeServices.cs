using AutoMapper;
using Data.Entities;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.Entities;
using Servicos.Services.Entities;
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
            var filmesAtivos = listadeFilmes.Where(f => f.Situacao == SituacaoFilme.Ativado);
            var filmesDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(filmesAtivos);
            return filmesDtos.OrderBy(nome => nome.Titulo);
        }

        public LerFilmeDto ConsultaPorId(int id)
        {
                var filme = _filmeDao.BuscarPorId(id);
                if (filme==null || filme.Situacao == SituacaoFilme.Arquivado)
                {
                    return null;
                }
                var filmeDto = _mapper.Map<LerFilmeDto>(filme);
                return filmeDto;
        }
        public LerFilmeDto BuscarFilmeCompleto(int id)
        {
            var filme = _filmeDao.BuscarPorFilmesCompletoID(id);
            if (filme == null || filme.Situacao == SituacaoFilme.Arquivado)
            {
                return null;
            }
            var filmeMapeado = _mapper.Map<LerFilmeDto>(filme);
            return filmeMapeado;
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

        public Result Altera(int id, AlterarFilmeDto obj)
        {
            var filmeSelecionado = _filmeDao.BuscarPorId(id);
            if (filmeSelecionado == null)
            {
                return Result.Fail("Filme não existe");
            }
            _mapper.Map(obj, filmeSelecionado);
            _filmeDao.Save();
            return Result.Ok();

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
            _filmeDao.Alterar(filmeSelecionado);
            //salvar o dao _filmedao.save(); error por conta que nao tem mais

        }
        public void ReativarFilme(int id)
        {
            var filmeSelecionado = _filmeDao.BuscarPorId(id);
            filmeSelecionado.Situacao = SituacaoFilme.Ativado;
            _filmeDao.Alterar(filmeSelecionado);
        }

        public IEnumerable<LerFilmeDto> BuscaFilmesArquivados()
        {
            var filmes = _filmeDao.BuscarTodos()
                .Where(f => f.Situacao == SituacaoFilme.Arquivado);
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmes);
            return filmesDto;


        }
    }
}
