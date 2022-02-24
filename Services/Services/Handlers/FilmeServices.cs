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

        public async Task<IEnumerable<LerFilmeDto>> ConsultaTodos(int skip, int take)
        {
            var listadeFilmes = await _filmeDao.BuscarTodos();
            var filmesAtivos = listadeFilmes.Where(f => f.Situacao == SituacaoEntities.Ativado)
                .Skip(skip).Take(take).ToList();
            var filmesDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(filmesAtivos);
            return (filmesDtos.OrderBy(nome => nome.Titulo));
        }
        
        public async Task<LerFilmeDto> ConsultaPorId(int id)
        {
                var filme = await _filmeDao.BuscarPorId(id);
                if (filme==null || filme.Situacao == SituacaoEntities.Arquivado)
                {
                    return null;
                }
                var filmeDto = _mapper.Map<LerFilmeDto>(filme);
                return filmeDto;
        }
        public async Task<LerFilmeDto> BuscarFilmeCompleto(int id)
        {
            var filme = await _filmeDao.BuscarPorFilmesCompletoID(id);
            if (filme == null || filme.Situacao == SituacaoEntities.Arquivado)
            {
                return null;
            }
            var filmeMapeado = _mapper.Map<LerFilmeDto>(filme);
            return filmeMapeado;
        }

        
        public async Task<Result> Cadastra(CriarFilmeDto obj)
        {
            var filme = _filmeDao.BuscarPorNome(obj.Titulo);
            if (filme != null)
            {
                return Result.Fail("Filme ja existe");
            }
            var filmeMapeado = _mapper.Map<Filme>(obj);
            await _filmeDao.Incluir(filmeMapeado);
            return Result.Ok();
        }

        public async Task<Result> Altera(int id, AlterarFilmeDto obj)
        {
            var filmeSelecionado = await _filmeDao.BuscarPorId(id);
            if (filmeSelecionado == null)
            {
                return Result.Fail("Filme não existe");
            }
            _mapper.Map(obj, filmeSelecionado);
            await _filmeDao.Save();
            return Result.Ok();

        }



        public async Task<Result> ArquivarFilme(int id)
        {
            var filmeSelecionado = await _filmeDao.BuscarPorId(id);
            if(filmeSelecionado == null || filmeSelecionado.Situacao==SituacaoEntities.Arquivado)
            {
                return Result.Fail("Filme nao existe");
            }

            filmeSelecionado.Situacao = SituacaoEntities.Arquivado;
            await _filmeDao.Alterar(filmeSelecionado);
            return Result.Ok();
            

        }
        public async Task<Result> ReativarFilme(int id)
        {
            var filmeSelecionado = await _filmeDao.BuscarPorId(id);
            if(filmeSelecionado==null || filmeSelecionado.Situacao == SituacaoEntities.Ativado)
            {
                return Result.Fail("Filme ja ativado ou Nao existe");
            }
            filmeSelecionado.Situacao = SituacaoEntities.Ativado;
            await _filmeDao.Alterar(filmeSelecionado);
            return Result.Ok();
        }

        public async Task<IEnumerable<LerFilmeDto>> BuscaFilmesArquivados(int skip, int take)
        {
            var filmes = await _filmeDao.BuscarTodos();
            var filmesPaginados = filmes
                .Where(f => f.Situacao == SituacaoEntities.Arquivado).Skip(skip).Take(take).ToList();
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmesPaginados);
            return filmesDto;


        }
        public async Task<Result> Excluir(int id)
        {

            var filmeSelecionado = await _filmeDao.BuscarPorId(id);
            if (filmeSelecionado == null)
            {
                return Result.Fail("Filme nao existe");
            }

            _filmeDao.Excluir(filmeSelecionado);
            return Result.Ok();
        }
    }
}
