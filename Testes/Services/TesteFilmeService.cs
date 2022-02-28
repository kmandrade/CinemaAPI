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
using Xunit;

namespace Testes.Services
{
    public class TesteFilmeService
    {
        private readonly IMapper _mapper;
        private readonly FilmeServices _filmeService;
        private readonly DiretorServices _diretorService;
        private readonly Mock<IFilmeDao> _filmeDao;
        private readonly Mock<IDiretorDao> _diretorDao;

        public TesteFilmeService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FilmeProfile());
                mc.AddProfile(new DiretorProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _filmeDao = new Mock<IFilmeDao>();
            _diretorDao = new Mock<IDiretorDao>();
            _filmeService = new FilmeServices(_mapper , _filmeDao.Object, _diretorDao.Object);
        }

        [Fact]
        public  async void BuscaFilmePorId_RetornaNullFilmeInexistente()
        {

            //Arrange
            int id = 2;
               _filmeDao.Setup(f => f.BuscarPorId(id)).ReturnsAsync(null as Filme);
            //Act
            var resultadoService=  _filmeService.ConsultaPorId(id);            
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
            var act = await _filmeService.ConsultaPorId(id);
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
            var act = await _filmeService.ConsultaPorId(id);
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
        public async void ConsultaTodosFilmes_RetornaNull_PaginaçãoErrada(int skip, int take)
        {
            //Arrange
              _filmeDao.Setup(f => f.BuscaTodos()).ReturnsAsync(null as IEnumerable<Filme>);
            //act
            var act = await _filmeService.BuscaTodos(skip, take);
            //assert
            Assert.Null(act);
        }


        [Fact]
        public async void ConsultaTodosFilmes_RetornaSomente_FilmesAtivosDto()
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

        [Fact]
        public async void CadastraFilme_RetornaNull_FilmeJaExistente()
        {
            //arrange
            var filmeDto = new List<CriarFilmeDto>()
            {
                new CriarFilmeDto(){ Titulo="filme", Duracao=100, DiretorId=2},
                new CriarFilmeDto(){Titulo="filme", Duracao=100, DiretorId=3}
            };
           
            var filme = _mapper.Map<IEnumerable<Filme>>(filmeDto);
            var filmesSelecionados=   _filmeDao.Setup(f=>f.BuscaTodos()).ReturnsAsync(filme).;

                _filmeDao.Setup(f => f.BuscarPorNome(filmesSelecionados.)).ReturnsAsync(null as Filme);
            
            
            
            //act
            var filmeService = await _filmeService.Cadastra(filmeDto[0]);

            //assert
            Assert.Null(filmeService);
        }
    }
}
