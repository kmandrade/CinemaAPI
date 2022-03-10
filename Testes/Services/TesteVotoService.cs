using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Domain.Dtos.VotosDto;
using Domain.Models;
using Domain.Profiles;
using Moq;
using Servicos.Services.Handlers;
using System.Threading.Tasks;
using Testes.BaseEntities;
using Xunit;

namespace Testes.Services
{
    public class TesteVotoService
    {
        private readonly IMapper _mapper;
        private readonly VotosServices _votosServices;
        private readonly Mock<IVotosRepository> _votosRepository;
        private readonly Mock<IFilmeRepository> _filmeRepository;

        public TesteVotoService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new VotosProfile());

                mc.AddProfile(new FilmeProfile());

            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _votosRepository = new Mock<IVotosRepository>();
            _filmeRepository = new Mock<IFilmeRepository>();

            _votosServices = new VotosServices(_votosRepository.Object, _mapper, _filmeRepository.Object);
        }

        [Fact]
        public async Task AdicionarVotosEmFilme_FilmeInexistente_RetornaFalse()
        {
            //arrange
            var adicionaVotos = new AdicionaVotosDto() { IdFilmeDto = 1, ValorDoVotoDto = 4 };
            _filmeRepository.Setup(f => f.BuscarPorId(1)).ReturnsAsync(null as Filme);

            //act
            var act = await _votosServices.AdicionarVotosEmFilme(adicionaVotos, 1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task AdicionarVotosEmFilme_BuscarVotoPorFilmeEUsuario_UsuarioJaVotouNoFilme_RetornaFalse()
        {
            //arrange
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "USUARIO" };
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var voto = new Votos() { IdUsuario = usuario.IdUsuario, ValorDoVoto = 4, IdVotos = 1, IdFilme = 1 };
            var adicionaVotosDto = _mapper.Map<AdicionaVotosDto>(voto);
            _filmeRepository.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            _votosRepository.Setup(v => v.BuscarVotoPorFilmeEUsuario(voto.IdFilme, usuario.IdUsuario)).ReturnsAsync(voto);
            //act
            var act = await _votosServices.AdicionarVotosEmFilme(adicionaVotosDto, usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AdicionarVotosEmFilme_UsuarioNaoVoltouNesseFilme_RetornaTrue()
        {
            //arrange
            var usuario = new Usuario() { IdUsuario = 1, NomeUsuario = "USUARIO" };
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var voto = new Votos() { IdUsuario = 2, ValorDoVoto = 4, IdVotos = 1, IdFilme = 1 };
            var adicionaVotosDto = _mapper.Map<AdicionaVotosDto>(voto);
            _filmeRepository.Setup(f => f.BuscarPorId(filme.IdFilme)).ReturnsAsync(filme);
            _votosRepository.Setup(v => v.BuscarVotoPorFilmeEUsuario(1, 1)).ReturnsAsync(null as Votos);

            //act
            var act = await _votosServices.AdicionarVotosEmFilme(adicionaVotosDto, usuario.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }
        [Fact]
        public async Task AlterarValorDoVotoEmFilme_FilmeNaoExite_RetornaFalse()
        {
            //arrange
            _votosRepository.Setup(v => v.BuscarVotoPorFilme(1)).ReturnsAsync(null as Votos);

            //act
            var act = await _votosServices.AlterarValorDoVotoEmFilme(1, 1, 1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);


        }
        [Fact]
        public async Task AlterarValorDoVotoEmFilme_UsuarioNaoVotouNoFilme_RetornaFalse()
        {
            //arrange
            var voto = new Votos() { IdUsuario = 1, ValorDoVoto = 3 };
            _votosRepository.Setup(v => v.BuscarVotoPorFilme(voto.IdFilme)).ReturnsAsync(voto);

            //act
            var act = await _votosServices.AlterarValorDoVotoEmFilme(voto.IdFilme, 1, 3);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarValorDoVotoEmFilme_VotoAlterado_RetornaTrue()
        {
            //arrange
            var voto = new Votos() { IdUsuario = 1, ValorDoVoto = 3, IdFilme = 1 };
            _votosRepository.Setup(v => v.BuscarVotoPorFilme(voto.IdFilme)).ReturnsAsync(voto);
            //act
            var act = await _votosServices.AlterarValorDoVotoEmFilme(voto.IdFilme, 4, voto.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }


        [Fact]
        public async Task ExcluirValorDoVotoDoFilme_FilmeNaoExiste_RetornaFalse()
        {
            //arrange
            _votosRepository.Setup(v => v.BuscarVotoPorFilme(1)).ReturnsAsync(null as Votos);

            //act
            var act = await _votosServices.ExcluirVotoDoFilme(1, 1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);

        }
        [Fact]
        public async Task ExcluirValorDoVotoDoFilme_UsuarioNaoVotouNoFilme_RetornaFalse()
        {
            //arrange
            var voto = new Votos() { IdUsuario = 1, ValorDoVoto = 3 };
            _votosRepository.Setup(v => v.BuscarVotoPorFilme(voto.IdFilme)).ReturnsAsync(voto);

            //act
            var act = await _votosServices.ExcluirVotoDoFilme(voto.IdFilme, 2);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);

        }

        [Fact]
        public async Task ExcluirValorDoVotoEmFilme_VotoExcluido_RetornaTrue()
        {
            //arrange
            var voto = new Votos() { IdUsuario = 1, ValorDoVoto = 3, IdFilme = 1 };
            _votosRepository.Setup(v => v.BuscarVotoPorFilme(voto.IdFilme)).ReturnsAsync(voto);
            //act
            var act = await _votosServices.ExcluirVotoDoFilme(voto.IdFilme, voto.IdUsuario);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }


    }
}
