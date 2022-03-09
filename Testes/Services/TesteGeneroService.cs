using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
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
        public async Task BuscarGeneroPorId_GeneroNaoExiste_RetornaFalse()
        {
            //arrage

            _generoRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(null as Genero);
            //act
            var act = await _generoServices.BuscarPorId(1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Genero(act);
            // Assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task BuscarGeneroPorId_GeneroEncontrado_RetornaTrue()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero" };
            _generoRepository.Setup(a => a.BuscarPorId(genero.IdGenero)).ReturnsAsync(genero);
            var generoDto = _mapper.Map<LerGeneroDto>(genero);
            //act
            var generoService = await _generoServices.BuscarPorId(generoDto.IdGenero);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Genero(generoService);
            //assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task BuscarTodosGeneros_RetornaNull_NenhumGeneroEncontrado()
        {
            //arrange
            _generoRepository.Setup(a => a.BuscarTodos()).ReturnsAsync(null as IEnumerable<Genero>);

            //act
            var generoService = await _generoServices.BuscarTodos(1, 2);
            //assert
            Assert.Null(generoService);
        }

       

        //Cadastrar Genero
        [Fact]
        public async Task CadastrarGenero_RetornaNUll_GeneroJaExiste()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero" };
            _generoRepository.Setup(a => a.BuscarPorNome(genero.NomeGenero)).ReturnsAsync(genero);
            var generoDto = _mapper.Map<CriarGeneroDto>(genero);
            //act
            var generoService = await _generoServices.Cadastrar(generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task CadastrarGenero_RetornaOk_Sucess()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero" };
            var generoDto = _mapper.Map<CriarGeneroDto>(genero);
            _generoRepository.Setup(a => a.Cadastrar(genero)).Returns(Task.FromResult(genero));
            //act
            var generoService = await _generoServices.Cadastrar(generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.True(resultado);
        }

        //Alterar Genero
        [Fact]
        public async Task AlterarGenero_RetornaFalse_GeneroNaoExiste()
        {
            //arrange
            _generoRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Genero);
            var generoDto = new AlterarGeneroDto() { NomeGenero = "Genero" };
            //act
            var generoService = await _generoServices.Alterar(1, generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task AlterarGenero_RetornaTrue_Sucess()
        {
            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "Genero" };
            var generoDto = _mapper.Map<AlterarGeneroDto>(genero);
            _generoRepository.Setup(a => a.BuscarPorId(genero.IdGenero)).Returns(Task.FromResult(genero));
            _generoRepository.Setup(a => a.Alterar(genero)).Returns(Task.FromResult(genero));

            //act
            var generoService = await _generoServices.Alterar(1, generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(generoService);
            //assert
            Assert.True(resultado);
        }

        //Exclui Genero
        [Fact]
        public async Task ExcluirGenero_RetornaFalse_GeneroNaoExiste()
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
        public async Task ExcluirGenero_RetornaTrue_GeneroExcluido()
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
