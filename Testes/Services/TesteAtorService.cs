using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Domain.Dtos.AtorDto;
using Domain.Models;
using Moq;
using Servicos.Services.Handlers;
using System.Collections.Generic;
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
        public async Task BuscarAtorPorId_AtorNaoExiste_RetornaFalse()
        {
            //arrage

            _atorRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Ator);
            //act
            var act = await _atorServices.BuscarPorId(1);
            var resultado = TestaTipoResultRepository<LerAtorDto>.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task BuscarAtorPorId_AtorEncontrado_RetornaTrue()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(a => a.BuscarPorId(ator.IdAtor)).ReturnsAsync(ator);
            var atorDto = _mapper.Map<LerAtorDto>(ator);
            //act
            var atorService = await _atorServices.BuscarPorId(atorDto.IdAtor);
            var resultado = TestaTipoResultRepository<LerAtorDto>.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task BuscarTodosAtores_RetornaNull_NenhumAtorEncontrado()
        {
            //arrange
            _atorRepository.Setup(a => a.BuscarTodos()).ReturnsAsync(null as IEnumerable<Ator>);

            //act
            var atorService = await _atorServices.BuscarTodos(1, 2);
            //assert
            Assert.Null(atorService);
        }



        //Cadastrar ATOR
        [Fact]
        public async Task CadastrarAtor_RetornaNUll_AtorJaExiste()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(a => a.BuscarPorNome(ator.NomeAtor)).ReturnsAsync(ator);
            var atorDto = _mapper.Map<CriarAtorDto>(ator);
            //act
            var atorService = await _atorServices.Cadastrarr(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task CadastrarAtor_RetornaOk_Sucess()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            var atorDto = _mapper.Map<CriarAtorDto>(ator);
            _atorRepository.Setup(a => a.Cadastrar(ator)).Returns(Task.FromResult(ator));
            //act
            var atorService = await _atorServices.Cadastrarr(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.True(resultado);
        }

        //Alterar ATOR
        [Fact]
        public async Task AlterarAtor_RetornaFalse_AtorNaoExiste()
        {
            //arrange
            _atorRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Ator);
            var atorDto = new AlterarAtorDto() { NomeAtor = "ator" };
            //act
            var atorService = await _atorServices.Alterar(1, atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task AlterarAtor_RetornaTrue_Sucess()
        {
            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            var atorDto = _mapper.Map<AlterarAtorDto>(ator);
            _atorRepository.Setup(a => a.BuscarPorId(ator.IdAtor)).Returns(Task.FromResult(ator));
            _atorRepository.Setup(a => a.Alterar(ator)).Returns(Task.FromResult(ator));

            //act
            var atorService = await _atorServices.Alterar(1, atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(atorService);
            //assert
            Assert.True(resultado);
        }


        //Exclui Ator
        [Fact]
        public async Task ExcluirAtor_RetornaFalse_AtorNaoExiste()
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
        public async Task ExcluirAtor_RetornaTrue_AtorExcluido()
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
