using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Data.Services.Handlers;
using Domain.Dtos.AtorDto;
using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Profiles;
using Moq;
using Servicos.Services.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testes.BaseEntities;
using Xunit;

namespace Testes.Services
{
    public class TesteAtorFilmeService
    {
        private readonly IMapper _mapper;
        private readonly AtorFilmeServices _atorFilmeServices;
        private readonly Mock<IAtorFilmeRepository> _atorFilmeRepository;
        private readonly Mock<IAtorRepository> _atorRepository;
        private readonly Mock<IFilmeRepository> _filmeRepository;
        public TesteAtorFilmeService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AtorFilmeProfile());
                mc.AddProfile(new AtorProfile());
                mc.AddProfile(new FilmeProfile());

            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _atorFilmeRepository = new Mock<IAtorFilmeRepository>();
            _atorRepository = new Mock<IAtorRepository>();
            _filmeRepository = new Mock<IFilmeRepository>();

            _atorFilmeServices = new AtorFilmeServices(_mapper, _atorFilmeRepository.Object, _atorRepository.Object, _filmeRepository.Object);
        }

        //BUSCA
        [Fact]
        public async Task BuscarFilmesPorAtor_RetornaNull_AtorNaoExiste()
        {
            //arrange 

            _atorFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarFilmesPorAtor(1)).ReturnsAsync(null as IEnumerable<AtoresFilme>);


            //act
            var act = await _atorFilmeServices.BuscarFilmesPorAtor(1);
            //assert
            Assert.Null(act);

        }
        [Fact]
        public async Task BuscarFilmesPorAtor_RetornaFilmes_Sucess()
        {

            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            var atoresFilmes = new List<AtoresFilme>() {
                    new AtoresFilme()
                    {
                        Filme = filme, Ator = ator, IdAtor = ator.IdAtor,
                        IdFilme = filme.IdFilme, IdAtoresFilme = 1
                    }
                 };
            _atorFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarFilmesPorAtor(ator.IdAtor)).ReturnsAsync(atoresFilmes);
            var filmeDto = _mapper.Map<LerFilmeDto>(filme);
            var atorDto= _mapper.Map<LerAtorDto>(ator);
            var atoresFilmesDto = _mapper.Map<IEnumerable<AtoresFilme>>(atoresFilmes);
            //act
            var atoresFilmeService= await _atorFilmeServices.BuscarFilmesPorAtor(atorDto.IdAtor);
            var primeioFilmeDoAtor = atoresFilmeService.FirstOrDefault(f => f.Filme.Titulo == filmeDto.Titulo);

            //assert
            Assert.Equal(filme.Titulo, primeioFilmeDoAtor.Filme.Titulo);

        }


        //ADICIONA
        [Fact]
        public async Task AdicionarAtorFilme_RetornaFalse_AtorNaoExiste()
        {
            //arrange 

            _atorRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Ator);
            var atorDto = new CriarAtorFilmeDto() { IdAtor=1, IdFilme=1 };
           
            //act
            var act = await _atorFilmeServices.AdicionarAtorFilme(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AdicionarAtorFilme_RetornaFalse_FilmeNaoExiste()
        {
            //arrange 

            
            var atorDto = new CriarAtorFilmeDto() { IdAtor = 1, IdFilme = 1 };
            _filmeRepository.Setup(f => f.BuscarPorId(1)).ReturnsAsync(null as Filme);
            //act
            var act = await _atorFilmeServices.AdicionarAtorFilme(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AdicionarAtorFilme_RetornaTrue_AtorEFilmeExistem()
        {
            //arrange 
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            var filme = new Filme() { Titulo = "filme", IdFilme = 1 };
            _atorRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(ator);
            _filmeRepository.Setup(f => f.BuscarPorId(1)).ReturnsAsync(filme);
            var atorDto = new CriarAtorFilmeDto() { IdAtor = 1, IdFilme = 1 };
            //act
            var act = await _atorFilmeServices.AdicionarAtorFilme(atorDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }


        //Alterar
        [Fact]
        public async Task AlterarAtorDoFilme_RetornaFalse_AtorNovoNaoExiste()
        {
            //arrange 
            _atorRepository.Setup(atoresFilme => atoresFilme.BuscarPorId(1)).ReturnsAsync(null as Ator);
            
            //act
            var act = await _atorFilmeServices.AlterarAtorDoFilme(1,1,1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarAtorDoFilme_RetornaFalse_AtorAtualEFilmeNaoExistem()
        {
            //arrange 
            var atorNovo = new Ator() { IdAtor = 1, NomeAtor = "ator" };
            _atorRepository.Setup(atoresFilme => atoresFilme.BuscarPorId(atorNovo.IdAtor)).ReturnsAsync(atorNovo);
            _atorFilmeRepository.Setup(atoresFilme=>atoresFilme.BuscarAtorEFilme(1,1)).ReturnsAsync(null as AtoresFilme);
            //act
            var act = await _atorFilmeServices.AlterarAtorDoFilme(6,6,atorNovo.IdAtor);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarAtorDoFilme_RetornaTrue_AtornovoExisteEAtorAtualEFilmeExistem()
        {
            //arrange 
            var atorNovo = new Ator() { IdAtor = 2, NomeAtor = "ator" };
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator2" };
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var atoresFilme = new AtoresFilme()
            {
                  IdAtor = ator.IdAtor,
                  IdFilme=filme.IdFilme,
                  
            };
            //verifica ator novo
            _atorRepository.Setup(atoresFilme => atoresFilme.BuscarPorId(2)).ReturnsAsync(atorNovo);
            //verificar se existe AtorEFilme
            _atorFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarAtorEFilme(ator.IdAtor, filme.IdFilme)).ReturnsAsync(atoresFilme);
            


            //act
            var act = await _atorFilmeServices.AlterarAtorDoFilme(ator.IdAtor,filme.IdFilme,atorNovo.IdAtor);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }

        //EXCLUIR
        [Fact]
        public async Task ExcluirAtorDoFilme_RetornaFalse_AtorEFilmeNaoExistem()
        {
            //arrange
            _atorFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarAtorEFilme(1, 1)).ReturnsAsync(null as AtoresFilme);

            //act
            var act = await _atorFilmeServices.DeletarAtorDoFilme(1, 1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ExcluirAtorDoFilme_RetornaTrue_AtorEFilmeExistem()
        {

            //arrange
            var ator = new Ator() { IdAtor = 1, NomeAtor = "ator2" };
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var atoresFilme = new AtoresFilme()
            {
                IdAtor = ator.IdAtor,
                IdFilme = filme.IdFilme,

            };
            _atorFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarAtorEFilme(1, 1)).ReturnsAsync(atoresFilme);

            //act
            var act = await _atorFilmeServices.DeletarAtorDoFilme(ator.IdAtor,filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }


    }
}
