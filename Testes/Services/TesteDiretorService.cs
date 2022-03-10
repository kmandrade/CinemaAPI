using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Domain.Dtos.DiretorDto;
using Domain.Models;
using Moq;
using Servicos.Services.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testes.BaseEntities;
using Xunit;

namespace Testes.Services
{
    public class TesteDiretorService
    {
        private readonly IMapper _mapper;
        private readonly DiretorServices _diretorServices;
        private readonly Mock<IDiretorRepository> _diretorRepository;

        public TesteDiretorService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DiretorProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _diretorRepository = new Mock<IDiretorRepository>();
            _diretorServices = new DiretorServices(_mapper, _diretorRepository.Object);
        }

        //BUSCAR DIRETOR

        [Fact]
        public async Task BuscarTodosDiretores_DiretoresNaoEncontrados_RetornaNull()
        {
            //arrage

            _diretorRepository.Setup(g => g.BuscarTodos()).ReturnsAsync(null as IEnumerable<Diretor>);
            //act
            var act = await _diretorServices.BuscarTodos(1,2);
            
            // Assert
            Assert.Null(act);
        }


        [Fact]
        public async Task BuscarDiretorPorId_DiretorNaoExiste_RetornaFalse()
        {
            //arrage

            _diretorRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(null as Diretor);
            //act
            var act = await _diretorServices.BuscarPorId(1);
            var resultado = TestaTipoResultRepository<LerDiretorDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.False(resultado);
        }
        //CADASTRAR DIRETOR
        [Fact]
        public async Task CadastrarDiretor_DiretorExistente_RetornaFalse()
        {
            //arrange
            var diretor = new Diretor() { NomeDiretor = "diretor", Id = 1 };
            var diretorDto=_mapper.Map<CriarDiretorDto>(diretor);
            _diretorRepository.Setup(d => d.BuscarDiretorPorNome(diretor.NomeDiretor))
                .ReturnsAsync(diretor);

            //act
            var act = await _diretorServices.Cadastrar(diretorDto);
            var resultado = TestaTipoResultRepository<CriarDiretorDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task CadastrarDiretor_DiretorCadastrado_RetornaTrue()
        {
            var diretor = new Diretor() { NomeDiretor = "diretor", Id = 1 };
            var diretorDto = _mapper.Map<CriarDiretorDto>(diretor);
            _diretorRepository.Setup(d => d.BuscarDiretorPorNome("nome"))
                .ReturnsAsync(null as Diretor);

            //act
            var act = await _diretorServices.Cadastrar(diretorDto);
            var resultado = TestaTipoResultRepository<CriarDiretorDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);

        }
        [Fact]
        public async Task AlterarDiretor_DiretorNaoExiste_RetornaFalse()
        {
            //arrage
            var diretorDto = new AlterarDiretorDto() { NomeDiretor = "diretor" };
            _diretorRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(null as Diretor);
            //act
            var act = await _diretorServices.Alterar(1, diretorDto);
            var resultado = TestaTipoResultRepository<AlterarDiretorDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task AlterarDiretor_DiretorEncontrado_RetornaTrue()
        {
            //arrage
            var diretor = new Diretor() { NomeDiretor = "diretor", Id = 1 };
            var diretorDto = _mapper.Map<AlterarDiretorDto>(diretor);
            _diretorRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(diretor);
            //act
            var act = await _diretorServices.Alterar(1, diretorDto);
            var resultado = TestaTipoResultRepository<AlterarDiretorDto>
                .Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.True(resultado);
        }
        [Fact]
        public async Task ExcluirDiretor_DiretorNaoEncontrado_RetornaFalse()
        {
            //arrage
            _diretorRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(null as Diretor);
            //act
            var act = await _diretorServices.Excluir(2);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ExcluirDiretor_DiretorEncontrado_RetornaTrue()
        {
            //arrage
            var diretor = new Diretor() { NomeDiretor = "diretor", Id = 1 };
            _diretorRepository.Setup(g => g.BuscarPorId(1)).ReturnsAsync(diretor);
            //act
            var act = await _diretorServices.Excluir(diretor.Id);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            // Assert
            Assert.True(resultado);
        }







    }
}
