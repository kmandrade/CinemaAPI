using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Data.Services.Handlers;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Profiles;
using Moq;
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

        private readonly Mock<IFilmeRepository> _filmeRepository;
        private readonly Mock<IDiretorRepository> _diretorRepository;

        public TesteFilmeService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FilmeProfile());
                mc.AddProfile(new DiretorProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _filmeRepository = new Mock<IFilmeRepository>();
            _diretorRepository = new Mock<IDiretorRepository>();
            _filmeService = new FilmeServices(_mapper, _filmeRepository.Object, _diretorRepository.Object);
        }


        //Bsuca Filme
        [Fact]
        public async Task BuscaFilmePorId_FilmeInexistente_RetornaFalse()
        {

            //Arrange
            int id = 2;
            _filmeRepository.Setup(f => f.BuscarPorId(id)).ReturnsAsync(null as Filme);
            //Act
            var resultadoService = await _filmeService.BuscarPorId(id);
            var resultado = TestaTipoResultRepository<LerNomeFilmeDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(resultadoService);
            // Assert
            Assert.False(resultado);
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarFilmePorId_SeFilmeArquivado_RetornaFalse(int id)
        {
            //Arrange

            var filmeDto = new LerFilmeDto()
            {
                IdFilme = id,
                Titulo = "aaa",
                Duracao = 100,
                Situacao = SituacaoEntities.Arquivado

            };
            var filme = _mapper.Map<Filme>(filmeDto);
            _filmeRepository.Setup(f => f.BuscarPorId(id)).ReturnsAsync(filme);
            //Act
            var act = await _filmeService.BuscarPorId(id);
            var resultado = TestaTipoResultRepository<LerNomeFilmeDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.False(resultado);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task BuscarFilmePorId_IdErrado_RetornaFalse(int id)
        {
            //arrage
            _filmeRepository.Setup(f => f.BuscarPorId(id)).ReturnsAsync(null as Filme);
            //act
            var act = await _filmeService.BuscarPorId(id);
            var resultado = TestaTipoResultRepository<LerNomeFilmeDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task BuscarFilmesArquivados_RetornaNUll_NenhumFilmeEncontrado()
        {
            //arrange
            var filmes = new List<Filme>()
            {
                new Filme(){ IdFilme=1, Titulo="filme1"},
                new Filme(){ IdFilme=2, Titulo="filme2"},
            };
            _filmeRepository.Setup(f => f.BuscarTodos()).ReturnsAsync(null as List<Filme>);
            //act
            var filmeService = await _filmeService.BuscarFilmesArquivados(1, 2);
            //assert
            Assert.Null(filmeService);
        }

        [Fact]
        public async Task BuscarFilmesPorDiretor_DiretorNaoExiste_RetornaNull()
        {
            //arrange
            _filmeRepository.Setup(f => f.BuscarFilmesPorDiretor(1)).ReturnsAsync(null as IEnumerable<Filme>);


            //act
            var act = await _filmeService.BuscarFilmesPorDiretor(1);
            //assert
            Assert.Null(act);

        }



        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task BuscarFilmePorIdCompleto_IdErrado_RetornaFalse(int id)
        {
            //arrage
            _filmeRepository.Setup(f => f.BuscarPorFilmesCompletoID(id)).ReturnsAsync(null as Filme);
            //act
            var act = await _filmeService.BuscarFilmeCompleto(id);
            var resultado = TestaTipoResultRepository<LerFilmeDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }



        [Fact]
        public async Task BuscarFilmeDetalhado_FilmeArquivado_RetornaFalse()
        {

            //arrange
            var filme = new Filme()
            {
                Titulo = "filme",
                IdFilme = 1,
                Situacao = SituacaoEntities.Arquivado
            };
            _filmeRepository.Setup(f => f.BuscarPorFilmesCompletoID(filme.IdFilme)).ReturnsAsync(null as Filme);
            //act
            var act = await _filmeService.BuscarFilmeCompleto(filme.IdFilme);
            var resultado = TestaTipoResultRepository<LerFilmeDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            
            //assert
            Assert.False(resultado);

        }


        //CadastrarFilme
        [Fact]
        public async Task CadastrarFilme_FilmeJaExistente_RetornaFalse()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", DiretorId = 1 };

            _filmeRepository.Setup(f => f.BuscarPorNome(filme.Titulo)).ReturnsAsync(filme);
            var filmeDto = _mapper.Map<CriarFilmeDto>(filme);

            //act
            var filmeService = await _filmeService.Cadastrar(filmeDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task CadastrarFilme_DiretorNaoexiste_RetornaFalse()
        {
            //arrange

            var filme = new CriarFilmeDto() { Titulo = "filme", Duracao = 100, DiretorId = 1 };
            _diretorRepository.Setup(d => d.BuscarPorId(1)).ReturnsAsync(null as Diretor);
            //act
            var filmeService = await _filmeService.Cadastrar(filme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task CadastrarFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { Titulo = "filme", DiretorId = 1, Duracao = 100, IdFilme = 1 };
            var filmeDto = _mapper.Map<CriarFilmeDto>(filme);
            var diretor = new Diretor() { Id = 1 };
            _diretorRepository.Setup(d => d.BuscarPorId(1)).ReturnsAsync(diretor);
            _filmeRepository.Setup(f => f.Cadastrar(filme)).Returns(Task.FromResult(filme));
            _filmeRepository.Setup(f => f.BuscarPorNome("filme")).Returns(Task.FromResult(null as Filme));

            //act
            var filmeService = await _filmeService.Cadastrar(filmeDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.True(resultado);
        }


        //AlterarFilme
        [Fact]
        public async Task AlterarFilme_FilmeNaoExiste_RetornaFalse()
        {
            //arrange

            var filme = new AlterarFilmeDto() { Titulo = "filme", Duracao = 100 };
            _filmeRepository.Setup(f => f.BuscarPorId(2)).ReturnsAsync(null as Filme);
            var filmeDto = _mapper.Map<AlterarFilmeDto>(filme);


            //act
            var filmeService = await _filmeService.Alterar(2, filmeDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarFilme_DiretorNaoExiste_RetornaFalse()
        {
            //arrange

            var filmeDto = new AlterarFilmeDto() { Titulo = "Filme", DiretorId = 1 };
            _diretorRepository.Setup(d => d.BuscarPorId(0)).ReturnsAsync(null as Diretor);
            //act
            var filmeService = await _filmeService.Alterar(1, filmeDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { Titulo = "filme", DiretorId = 1, Duracao = 100, IdFilme = 1 };
            var filmeDto = _mapper.Map<AlterarFilmeDto>(filme);
            var diretor = new Diretor() { Id = 1 };
            _diretorRepository.Setup(d => d.BuscarPorId(filme.DiretorId)).ReturnsAsync(diretor);
            _filmeRepository.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            _filmeRepository.Setup(f => f.Alterar(filme)).Returns(Task.FromResult(filme));


            //act
            var filmeService = await _filmeService.Alterar(1, filmeDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.True(resultado);
        }


        //Arquivar Filme
        [Fact]
        public async Task ArquivarFilme_RetornaFail_FilmeNaoExiste()
        {
            //arrange

            _filmeRepository.Setup(f => f.BuscarPorId(0)).ReturnsAsync(null as Filme);

            //act

            var filmeService = await _filmeService.ArquivarFilme(0);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ArquivarFilme_RetornaFail_FilmeJaArquivado()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Arquivado };
            _filmeRepository.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);

            //act
            var filmeService = await _filmeService.ArquivarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task ArquivarFilme_RetornaTrue_Sucesso()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Ativado };
            _filmeRepository.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            //act
            var filmeService = await _filmeService.ArquivarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.True(resultado);
        }

        //Reativar Filme
        [Fact]
        public async Task ReativarFilme_RetornaFail_FilmeNaoExiste()
        {
            //arrange

            _filmeRepository.Setup(f => f.BuscarPorId(0)).ReturnsAsync(null as Filme);

            //act

            var filmeService = await _filmeService.ReativarFilme(0);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ReativarFilme_RetornaFail_FilmeJaAtivado()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Ativado };
            _filmeRepository.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);

            //act
            var filmeService = await _filmeService.ReativarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ReativarFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Arquivado };
            _filmeRepository.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            //act
            var filmeService = await _filmeService.ReativarFilme(filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.True(resultado);
        }

        //Excluir Filme
        [Fact]
        public async Task ExcluirFilme_RetornaFail_FilmeNaoExiste()
        {
            //arrange

            _filmeRepository.Setup(f => f.BuscarPorId(0)).ReturnsAsync(null as Filme);

            //act

            var filmeService = await _filmeService.Excluir(0);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);

            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ExcluirFilme_RetornaFail_FilmeAtivo()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Ativado };
            _filmeRepository.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);

            //act
            var filmeService = await _filmeService.Excluir(filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ExcluirFilme_RetornaOk_Sucess()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme", Situacao = SituacaoEntities.Arquivado };
            _filmeRepository.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            //act
            var filmeService = await _filmeService.Excluir(filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(filmeService);
            //assert
            Assert.True(resultado);
        }


    }
}
