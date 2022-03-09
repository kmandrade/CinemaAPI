using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.InterfacesService;
using FluentResults;

namespace Data.Services.Handlers
{
    public class FilmeServices : IFilmeService
    {

        private readonly IFilmeRepository _filmeRepository;
        private readonly IDiretorRepository _diretorRepository;
        
        private readonly IMapper _mapper;

        //preciso dizer pra minha aplicação que ela deve fazer o mapeamento
        //implementar o mapeamento, interfaces e utilizar o filmeRepository para ter acesso ao banco pela interface
        public FilmeServices(IMapper mapper, IFilmeRepository filmeRepository, IDiretorRepository diretorRepository)
        {
            _filmeRepository = filmeRepository;
            _mapper = mapper;
            _diretorRepository = diretorRepository;
            
        }

        public async Task<IEnumerable<LerFilmeDto>> BuscarTodos(int skip, int take)
        {
            
            var listadeFilmes = await _filmeRepository.BuscarTodos();
            
            if(listadeFilmes == null)
            {
                return null;
            }
                           
            var filmesDtos = _mapper.Map<IEnumerable<LerFilmeDto>>(listadeFilmes);
            return (filmesDtos.Skip(skip).Take(take).OrderBy(nome => nome.Titulo));
        }
        
        public async Task<Result<LerNomeFilmeDto>> BuscarPorId(int id)
        {
                var filme = await _filmeRepository.BuscarPorId(id);
                
                if (filme==null || filme.Situacao == SituacaoEntities.Arquivado)
                {
                    return Result.Fail("Filme nao encontrado");
                }
                var filmeDto = _mapper.Map<LerNomeFilmeDto>(filme);
                return Result.Ok(filmeDto);
        }
        public async Task<LerFilmeDto> BuscarFilmeCompleto(int id)
        {
            var filme = await _filmeRepository.BuscarPorFilmesCompletoID(id);
            if (filme == null || filme.Situacao == SituacaoEntities.Arquivado)
            {
                return null;
            }
            var filmeMapeado = _mapper.Map<LerFilmeDto>(filme);
            return filmeMapeado;
        }

        public async Task<IEnumerable<LerNomeFilmeDto>> BuscarFilmesMaisVotados()
        {
            //retorna somente filmes ativos
            var filmes = await _filmeRepository.BuscarFilmesMaisVotados();

            var filmesDto = _mapper.Map<IEnumerable<LerNomeFilmeDto>>(filmes);
            return filmesDto;
        }


        public async Task<Result> Cadastrar(CriarFilmeDto obj)
        {
            
            var diretorSelecionado = await _diretorRepository.BuscarPorId(obj.DiretorId);
            if(diretorSelecionado == null )
            {
                return null;
            }
            var filmeSelecionado = await _filmeRepository.BuscarPorNome(obj.Titulo);
            if (filmeSelecionado != null)
            {
                return null;
            }
            var filmeMapeado = _mapper.Map<Filme>(obj);
            await _filmeRepository.Cadastrar(filmeMapeado);
            return Result.Ok();

        }

        public async Task<Result> Alterar(int id, AlterarFilmeDto obj)
        {
            var filmeSelecionado = await _filmeRepository.BuscarPorId(id);
            var diretorSelecionado = await _diretorRepository.BuscarPorId(obj.DiretorId);
            if (diretorSelecionado == null || filmeSelecionado==null)
            {
                return null;
            }
            
            _mapper.Map(obj, filmeSelecionado);
            await _filmeRepository.Save();
            return Result.Ok();

        }

        public async Task<Result> ArquivarFilme(int id)
        {
            var filmeSelecionado = await _filmeRepository.BuscarPorId(id);
            if(filmeSelecionado == null || filmeSelecionado.Situacao==SituacaoEntities.Arquivado)
            {
                return Result.Fail("Filme nao existe");
            }

            filmeSelecionado.Situacao = SituacaoEntities.Arquivado;
            
            await _filmeRepository.Alterar(filmeSelecionado);
            return Result.Ok();
            

        }
        public async Task<Result> ReativarFilme(int id)
        {
            var filmeSelecionado = await _filmeRepository.BuscarPorId(id);
            if(filmeSelecionado==null || filmeSelecionado.Situacao == SituacaoEntities.Ativado)
            {
                return Result.Fail("Filme ja ativado ou Nao existe");
            }
            filmeSelecionado.Situacao = SituacaoEntities.Ativado;
            await _filmeRepository.Alterar(filmeSelecionado);
            return Result.Ok();
        }

        public async Task<IEnumerable<LerFilmeDto>> BuscarFilmesArquivados(int skip, int take)
        {
            if (skip <= 0 || take <= 0)
            {
                return null;
            }
            var filmes = await _filmeRepository.BuscarTodos();
            if (filmes == null)
            {
                return null;
            }
            var filmesPaginados = filmes
                .Where(f => f.Situacao == SituacaoEntities.Arquivado).Skip(skip).Take(take).ToList();
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmesPaginados);
            return filmesDto;

        }
        public async Task<Result> Excluir(int id)
        {

            var filmeSelecionado = await _filmeRepository.BuscarPorId(id);
            if (filmeSelecionado == null  || filmeSelecionado.Situacao==SituacaoEntities.Ativado)
            {
                return Result.Fail("Filme nao existe ou Ativo");
            }
            _filmeRepository.Excluir(filmeSelecionado);

            
            return Result.Ok();
        }
    }
}
