using AutoMapper;
using Cinema.Api.Profiles;
using Data.Entities;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using Moq;
using Servicos.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.BaseEntities;
using Xunit;

namespace Testes.Services
{
    public class TesteGeneroService
    {
        private readonly IMapper _mapper;
        private readonly GeneroServices _generoServices;
        private readonly Mock<IGeneroRepository> _generoRepository;

        public TesteGeneroService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeneroProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _generoRepository = new Mock<IGeneroRepository>();
            _generoServices= new GeneroServices(_mapper,_generoRepository.Object);
        }

        //BUSCA GENERO
        [Fact]
        public async void BuscaGeneroPorId_RetornaNUll_GeneroNaoExiste()
        {
            //arrage

            _generoRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(null as Genero);
            //act
            var act = await _generoServices.BuscaPorId(1);
            //assert
            Assert.Null(act);
        }
        [Fact]
        public async void BuscaGeneroPorId_RetornaGeneroDto_Sucess()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero" };
            _generoRepository.Setup(a => a.BuscarPorId(genero.IdGenero)).ReturnsAsync(genero);
            var generoDto = _mapper.Map<LerGeneroDto>(genero);
            //act
            var generoService = await _generoServices.BuscaPorId(generoDto.IdGenero);
            //assert
            Assert.Equal(generoService.NomeGenero, generoDto.NomeGenero);
        }

        [Fact]
        public async void BuscaTodosGeneros_RetornaNull_NenhumGeneroEncontrado()
        {
            //arrange
            _generoRepository.Setup(a => a.BuscaTodos()).ReturnsAsync(null as IEnumerable<Genero>);

            //act
            var generoService = await _generoServices.BuscaTodos(1, 2);
            //assert
            Assert.Null(generoService);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        public async void BuscaTodosGeneros_RetornaNull_PaginacaoErrada(int skip, int take)
        {
            //Arrange
            _generoRepository.Setup(a => a.BuscaTodos()).ReturnsAsync(null as IEnumerable<Genero>);
            //act
            var act = await _generoServices.BuscaTodos(skip, take);
            //assert
            Assert.Null(act);
        }

        //CADASTRA Genero
        [Fact]
        public async void CadastraGenero_RetornaNUll_GeneroJaExiste()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero" };
            _generoRepository.Setup(a => a.BuscaPorNome(genero.NomeGenero)).ReturnsAsync(genero);
            var generoDto = _mapper.Map<CriarGeneroDto>(genero);
            //act
            var generoService = await _generoServices.Cadastra(generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async void CadastraGenero_RetornaOk_Sucess()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero" };
            var generoDto = _mapper.Map<CriarGeneroDto>(genero);
            _generoRepository.Setup(a => a.Cadastra(genero)).Returns(Task.FromResult(genero));
            //act
            var generoService = await _generoServices.Cadastra(generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.True(resultado);
        }

        //ALTERA Genero
        [Fact]
        public async void AlteraGenero_RetornaFalse_GeneroNaoExiste()
        {
            //arrange
            _generoRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Genero);
            var generoDto = new AlterarGeneroDto() { NomeGenero = "Genero" };
            //act
            var generoService = await _generoServices.Altera(1, generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async void AlteraGenero_RetornaTrue_Sucess()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "Genero" };
            var generoDto = _mapper.Map<AlterarGeneroDto>(genero);
            _generoRepository.Setup(a => a.BuscarPorId(genero.IdGenero)).Returns(Task.FromResult(genero));
            _generoRepository.Setup(a => a.Alterar(genero)).Returns(Task.FromResult(genero));

            //act
            var generoService = await _generoServices.Altera(1, generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.True(resultado);
        }

        //Exclui Genero
        [Fact]
        public async void ExcluiGenero_RetornaFalse_GeneroNaoExiste()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "Genero" };
            _generoRepository.Setup(a => a.BuscarPorId(genero.IdGenero)).ReturnsAsync(null as Genero);
            //act
            var generoService = await _generoServices.Excluir(genero.IdGenero);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ExcluiGenero_RetornaTrue_GeneroExcluido()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "Genero" };
            _generoRepository.Setup(a => a.BuscarPorId(genero.IdGenero)).ReturnsAsync(genero);
            _generoRepository.Setup(a => a.Excluir(genero));
            //act
            var generoService = await _generoServices.Excluir(genero.IdGenero);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.True(resultado);
        }


    }
}
