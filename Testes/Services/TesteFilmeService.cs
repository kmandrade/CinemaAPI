using AutoMapper;
using Cinema.Api.Profiles;
using Data.Entities;
using Data.Services.Handlers;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Profiles;
using Domain.Services.Entities;
using FluentResults;
using Moq;
using Servicos.Services.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testes.BaseEntities;
using Xunit;

namespace Testes.Services
{
    public class TesteFilmeService
    {
        private readonly IMapper _mapper;
        private readonly FilmeServices _filmeService;
        private readonly DiretorServices _diretorService;
        private readonly Mock<IFilmeRepository> _filmeDao;
        private readonly Mock<IDiretorRepository> _diretorDao;

        public TesteFilmeService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FilmeProfile());
                mc.AddProfile(new DiretorProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _filmeDao = new Mock<IFilmeRepository>();
            _diretorDao = new Mock<IDiretorRepository>();
            _filmeService = new FilmeServices(_mapper , _filmeDao.Object, _diretorDao.Object);
        }

        [Fact]
        public  async void BuscaFilmePorId_RetornaNullFilmeInexistente()
        {

            //Arrange
            int id = 2;
               _filmeDao.Setup(f => f.BuscarPorId(id)).ReturnsAsync(null as Filme);
            //Act
            var resultadoService=  _filmeService.BuscaPorId(id);            
            // Assert
            Assert.Null(resultadoService.Result);
        }
        [Theory]
        [InlineData(1)]
        public async void BuscaFilmePorId_Retorna_NUll_SeFilmeArquivado(int id)
        {
            //Arrange
             
            var filmeDto = new LerFilmeDto()
            {
                IdFilme = id,
                Titulo="aaa",
                Duracao=100,
                Situacao=SituacaoEntities.Arquivado
               
            };
            var filme = _mapper.Map<Filme>(filmeDto);
            _filmeDao.Setup(f=>f.BuscarPorId(id)).ReturnsAsync(filme);
            //Act
            var act = await _filmeService.BuscaPorId(id);
            //Assert
            Assert.Null(act);//ou tira o await e bota act.Result
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void BuscaFilmePorId_Retorna_Null_IdErrado(int id)
        {
            //arrage
            _filmeDao.Setup(f => f.BuscarPorId(id)).ReturnsAsync(null as Filme);
            //act
            var act = await _filmeService.BuscaPorId(id);
            //assert
            Assert.Null(act);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void BuscaFilmePorIdCompleto_Retorna_Null_IdErrado(int id)
        {
            //arrage
            _filmeDao.Setup(f => f.BuscarPorFilmesCompletoID(id)).ReturnsAsync(null as Filme);
            //act
            var act = await _filmeService.BuscaFilmeCompleto(id);
            //assert
            Assert.Null(act);
        }


        [Theory]
        [InlineData(0,0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        public async void BuscaTodosFilmes_RetornaNull_PaginaçãoErrada(int skip, int take)
        {
            //Arrange
              _filmeDao.Setup(f => f.BuscaTodos()).ReturnsAsync(null as IEnumerable<Filme>);
            //act
            var act = await _filmeService.BuscaTodos(skip, take);
            //assert
            Assert.Null(act);
        }

        [Fact]
        public async void BuscaTodosFilmes_RetornaSomente_FilmesAtivosDto()
        {
            //arrange
            var filmesDto = new List<LerFilmeDto>()
            {
                new LerFilmeDto(){ IdFilme=1, Titulo="filme1", Situacao=SituacaoEntities.Ativado},
                new LerFilmeDto(){ IdFilme=2, Titulo="filme2", Situacao=SituacaoEntities.Arquivado}
            };
            var filmes = _mapper.Map<IEnumerable<Filme>>(filmesDto);
            var filmesEncontrados =  _filmeDao.Setup(f=>f.BuscaTodos()).ReturnsAsync(filmes);
            //act
            int skip = 1, take = 2;
            var filmesEncontradosAtivosDto = await _filmeService.BuscaTodos(skip, take);
             bool FilmesAtivosDto()
            {
                foreach(var filme in filmesEncontradosAtivosDto)
                {
                    if (filme.Situacao == SituacaoEntities.Ativado)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            //assert
            Assert.True(FilmesAtivosDto());            
          
        }

        [Fact]
        public async void BuscaFilmeDetalhado_RetornaNull_IdErrado()
        {
            //arrange
            int id = -1;
            _filmeDao.Setup(f => f.BuscarPorFilmesCompletoID(id)).ReturnsAsync(null as Filme);

            //act
            var filmeEncontrado= await _filmeService.BuscaFilmeCompleto(id);
            //assert
            Assert.Null(filmeEncontrado);
        }
        
        [Fact]
        public async void BuscaFilmeDetalhado_RetornaNUll_FilmeArquivado()
        {

            //arrange
            var filme = new Filme()
            {
                Titulo = "filme",
                IdFilme = 1,
                Situacao = SituacaoEntities.Arquivado
            };
            _filmeDao.Setup(f => f.BuscarPorFilmesCompletoID(filme.IdFilme)).ReturnsAsync(null as Filme);
            //act
            var filmeEncontrado = await _filmeService.BuscaFilmeCompleto(filme.IdFilme);
            //assert
            Assert.Null(filmeEncontrado);

        }


        //CadastraFilme
        [Fact]
        public async void CadastraFilme_RetornaNull_FilmeJaExistente()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo="filme", DiretorId=1 };

             _filmeDao.Setup(f => f.BuscarPorNome(filme.Titulo)).ReturnsAsync(filme);
            var filmeDto = _mapper.Map<CriarFilmeDto>(filme);
           
            //act
            var filmeService = await _filmeService.Cadastra(filmeDto);

            //assert
            Assert.Null(filmeService);
        }
        [Fact]
        public async void CadastraFilme_RetornaNull_DiretorNaoexiste()
        {
            //arrange
            
            var filme = new CriarFilmeDto() { Titulo="filme", Duracao = 100 , DiretorId=1 };
            _diretorDao.Setup(d => d.BuscarPorId(1)).ReturnsAsync(null as Diretor);
            //act
            var filmeService= await _filmeService.Cadastra(filme);
            //assert
            Assert.Null(filmeService);
        }
        [Fact]
        public async void CadastraFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { Titulo = "filme", DiretorId = 1, Duracao = 100,IdFilme=1 };
            var filmeDto = _mapper.Map<CriarFilmeDto>(filme);
            var diretor = new Diretor() { Id = 1 };
            _diretorDao.Setup(d=>d.BuscarPorId(1)).ReturnsAsync(diretor);
            _filmeDao.Setup(f => f.Cadastra(filme)).Returns(Task.FromResult(filme));
            _filmeDao.Setup(f => f.BuscarPorNome("filme")).Returns(Task.FromResult(null as Filme));
            
            //act
            var filmeService = await _filmeService.Cadastra(filmeDto);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.True(resultado);
        }


        //AlteraFilme
        [Fact]
        public async void AlteraFilme_RetornaNull_FilmeNaoExiste()
        {
            //arrange
            
            var filme = new AlterarFilmeDto() {  Titulo = "filme", Duracao=100 };
            _filmeDao.Setup(f => f.BuscarPorId(2)).ReturnsAsync(null as Filme);
            var filmeDto = _mapper.Map<AlterarFilmeDto>(filme);

            
            //act
            var filmeService= await _filmeService.Altera(2,filmeDto);
            
            //assert
            Assert.Null(filmeService);
        }
        [Fact]
        public async void AlteraFilme_RetornaNull_DiretorNaoExiste()
        {
            //arrange
            
            var filmeDto= new AlterarFilmeDto() { Titulo="Filme", DiretorId=1 };
            _diretorDao.Setup(d => d.BuscarPorId(0)).ReturnsAsync(null as Diretor);
            //act
            var filmeService= await _filmeService.Altera(1, filmeDto);

            //assert
            Assert.Null(filmeService);
        }
        [Fact]
        public async void AlteraFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { Titulo = "filme", DiretorId = 1, Duracao = 100, IdFilme = 1 };
            var filmeDto = _mapper.Map<AlterarFilmeDto>(filme);
            var diretor = new Diretor() { Id = 1 };
            _diretorDao.Setup(d => d.BuscarPorId(filme.DiretorId)).ReturnsAsync(diretor);
            _filmeDao.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            _filmeDao.Setup(f => f.Alterar(filme)).Returns(Task.FromResult(filme));
            

            //act
            var filmeService = await _filmeService.Altera(1,filmeDto);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.True(resultado);
        }


        //Arquivar Filme
        [Fact]
        public async void ArquivaFilme_RetornaFail_FilmeNaoExiste()
        {
            //arrange
            
            _filmeDao.Setup(f => f.BuscarPorId(0)).ReturnsAsync(null as Filme);

            //act

            var filmeService = await _filmeService.ArquivarFilme(0);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ArquivaFilme_RetornaFail_FilmeJaArquivado()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme",Situacao=SituacaoEntities.Arquivado};
            _filmeDao.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);
            
            //act
            var filmeService= await _filmeService.ArquivarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async void ArquivaFilme_RetornaTrue_Sucesso()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Ativado };
            _filmeDao.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            //act
            var filmeService = await _filmeService.ArquivarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.True(resultado);
        }
        
        //Reativar Filme
        [Fact]
        public async void ReativarFilme_RetornaFail_FilmeNaoExiste()
        {
            //arrange

            _filmeDao.Setup(f => f.BuscarPorId(0)).ReturnsAsync(null as Filme);

            //act

            var filmeService = await _filmeService.ReativarFilme(0);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ReativarFilme_RetornaFail_FilmeJaAtivado()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Ativado };
            _filmeDao.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);

            //act
            var filmeService = await _filmeService.ReativarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ReativarFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Arquivado };
            _filmeDao.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            //act
            var filmeService = await _filmeService.ReativarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.True(resultado);
        }

        //Excluir Filme
        [Fact]
        public async void ExcluirFilme_RetornaFail_FilmeNaoExiste()
        {
            //arrange

            _filmeDao.Setup(f => f.BuscarPorId(0)).ReturnsAsync(null as Filme);

            //act

            var filmeService = await _filmeService.Excluir(0);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ExcluirFilme_RetornaFail_FilmeAtivo()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Ativado };
            _filmeDao.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);

            //act
            var filmeService = await _filmeService.Excluir(filme.IdFilme);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ExcluirFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Arquivado };
            _filmeDao.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            //act
            var filmeService = await _filmeService.Excluir(filme.IdFilme);
            var resultado = TesteRepository.Retorna_True_OU_False_Result(filmeService);
            //assert
            Assert.True(resultado);
        }


    }
}
