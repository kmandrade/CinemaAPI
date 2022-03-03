using AutoMapper;
using Cinema.Api.Profiles;
using Data.Entities;
using Domain.Dtos.AtorDto;
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
    public class TesteAtorService
    {
        private readonly IMapper _mapper;
        private readonly AtorServices _atorServices;
        private readonly Mock<IAtorRepository> _atorRepository;

        public TesteAtorService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AtorProfile());
                
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _atorRepository = new Mock<IAtorRepository>();
            _atorServices = new AtorServices(_mapper, _atorRepository.Object);
        }


        //BUSCA ATOR
        [Fact]
        public async void BuscaAtorPorId_RetornaNUll_AtorNaoExiste()
        {
            //arrage

            _atorRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Ator);
            //act
            var act = await _atorServices.BuscaPorId(1);
            //assert
            Assert.Null(act);
        }
        [Fact]
        public async void BuscaAtorPorId_RetornaAtorDto_Sucess()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(a => a.BuscarPorId(ator.IdAtor)).ReturnsAsync(ator);
            var atorDto = _mapper.Map<LerAtorDto>(ator);
            //act
            var atorService= await _atorServices.BuscaPorId(atorDto.IdAtor);
            //assert
            Assert.Equal(atorService.NomeAtor,atorDto.NomeAtor);
        }

        [Fact]
        public async void BuscaTodosAtores_RetornaNull_NenhumAtorEncontrado()
        {
            //arrange
            _atorRepository.Setup(a => a.BuscaTodos()).ReturnsAsync(null as IEnumerable<Ator>);

            //act
            var atorService = await _atorServices.BuscaTodos(1,2);
            //assert
            Assert.Null(atorService);
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        public async void BuscaTodosAtores_RetornaNull_PaginacaoErrada(int skip,int take)
        {
            //Arrange
            _atorRepository.Setup(a => a.BuscaTodos()).ReturnsAsync(null as IEnumerable<Ator>);
            //act
            var act = await _atorServices.BuscaTodos(skip, take);
            //assert
            Assert.Null(act);
        }


        //CADASTRA ATOR
        [Fact]
        public async void CadastraAtor_RetornaNUll_AtorJaExiste()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(a => a.BuscarPorNome(ator.NomeAtor)).ReturnsAsync(ator);
            var atorDto = _mapper.Map<CriarAtorDto>(ator);
            //act
            var atorService = await _atorServices.Cadastra(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async void CadastraAtor_RetornaOk_Sucess()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            var atorDto = _mapper.Map<CriarAtorDto>(ator);
            _atorRepository.Setup(a => a.Cadastra(ator)).Returns(Task.FromResult(ator));
            //act
            var atorService= await _atorServices.Cadastra(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.True(resultado);
        }

        //ALTERA ATOR
        [Fact]
        public async void AlteraAtor_RetornaFalse_AtorNaoExiste()
        {
            //arrange
            _atorRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Ator);
            var atorDto = new AlterarAtorDto() { NomeAtor="ator" };
            //act
            var atorService = await _atorServices.Altera(1, atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.False(resultado);
            
        }
        [Fact]
        public async void AlteraAtor_RetornaTrue_Sucess()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            var atorDto = _mapper.Map<AlterarAtorDto>(ator);
            _atorRepository.Setup(a => a.BuscarPorId(ator.IdAtor)).Returns(Task.FromResult(ator));
            _atorRepository.Setup(a => a.Alterar(ator)).Returns(Task.FromResult(ator));

            //act
            var atorService= await _atorServices.Altera(1, atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.True(resultado);
        }


        //Exclui Ator
        [Fact]
        public async void ExcluiAtor_RetornaFalse_AtorNaoExiste()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(a => a.BuscarPorId(ator.IdAtor)).ReturnsAsync(null as Ator);
            //act
            var atorService = await _atorServices.Excluir(ator.IdAtor);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async void ExcluiAtor_RetornaTrue_AtorExcluido()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(a => a.BuscarPorId(ator.IdAtor)).ReturnsAsync(ator);
            _atorRepository.Setup(a => a.Excluir(ator));
            //act
            var atorService = await _atorServices.Excluir(ator.IdAtor);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.True(resultado);
        }
    }
}
