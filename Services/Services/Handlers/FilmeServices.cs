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

        IFilmeRepository _filmeDao;
        IDiretorRepository _diretorDao;
       
        private readonly IMapper _mapper;

        //preciso dizer pra minha aplicação que ela deve fazer o mapeamento
        //implementar o mapeamento, interfaces e utilizar o filmedao para ter acesso ao banco pela interface
        public FilmeServices(IMapper mapper, IFilmeRepository filmeDao,  IDiretorRepository diretorDao)
        {
            _filmeDao = filmeDao;
            _mapper = mapper;
            _diretorDao = diretorDao;
        }

        public async Task<IEnumerable<LerFilmeDto>> BuscaTodos(int skip, int take)
        {
            if(skip<=0 || take <= 0)
            {
                return null;
            }
            var listadeFilmes = await _filmeDao.BuscaTodos();
            var filmesAtivos = listadeFilmes.Where(f => f.Situacao == SituacaoEntities.Ativado).ToList();
            if(filmesAtivos == null)
            {
                return null;
            }
            var filmesPaginados = filmesAtivos.Skip(skip).Take(take);                
            var filmesDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(filmesAtivos);
            return (filmesDtos.OrderBy(nome => nome.Titulo));
        }
        
        public async Task<LerFilmeDto> BuscaPorId(int id)
        {
                var filme = await _filmeDao.BuscarPorId(id);
                
                if (filme==null || filme.Situacao == SituacaoEntities.Arquivado)
                {
                    return null;
                }
                var filmeDto = _mapper.Map<LerFilmeDto>(filme);
                return filmeDto;
        }
        public async Task<LerFilmeDto> BuscaFilmeCompleto(int id)
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
            
            var diretorSelecionado = await _diretorDao.BuscarPorId(obj.DiretorId);
            if(diretorSelecionado == null )
            {
                return null;
            }
            var filmeSelecionado = await _filmeDao.BuscarPorNome(obj.Titulo);
            if (filmeSelecionado != null)
            {
                return null;
            }
            var filmeMapeado = _mapper.Map<Filme>(obj);
            await _filmeDao.Cadastra(filmeMapeado);
            return Result.Ok();

        }

        public async Task<Result> Altera(int id, AlterarFilmeDto obj)
        {
            var filmeSelecionado = await _filmeDao.BuscarPorId(id);
            var diretorSelecionado = await _diretorDao.BuscarPorId(obj.DiretorId);
            if (diretorSelecionado == null || filmeSelecionado==null)
            {
                return null;
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
            var filmes = await _filmeDao.BuscaTodos();
            var filmesPaginados = filmes
                .Where(f => f.Situacao == SituacaoEntities.Arquivado).Skip(skip).Take(take).ToList();
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmesPaginados);
            return filmesDto;

        }
        public async Task<Result> Excluir(int id)
        {

            var filmeSelecionado = await _filmeDao.BuscarPorId(id);
            if (filmeSelecionado == null  || filmeSelecionado.Situacao==SituacaoEntities.Ativado)
            {
                return Result.Fail("Filme nao existe ou Ativo");
            }

            _filmeDao.Excluir(filmeSelecionado);
            return Result.Ok();
        }
    }
}
