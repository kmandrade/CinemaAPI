using AutoMapper;
using Cinema.Api.Profiles;
using Data.InterfacesData;
using Data.Services.Handlers;
using Domain.Dtos.FilmeDto;
using Domain.Dtos.FilmeGenero;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using Domain.Profiles;
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
    public class TesteGeneroFilmeService
    {
        private readonly IMapper _mapper;
        private readonly GeneroFilmeServices _generoFilmeServices;
        private readonly Mock<IGeneroFilmeRepository> _generoFilmeRepository;
        private readonly Mock<IGeneroRepository> _generoRepository;
        private readonly Mock<IFilmeRepository> _filmeRepository;

        public TesteGeneroFilmeService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeneroFilmeProfile());
                mc.AddProfile(new GeneroProfile());
                mc.AddProfile(new FilmeProfile());

            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _generoFilmeRepository = new Mock<IGeneroFilmeRepository>();
            _generoRepository = new Mock<IGeneroRepository>();
            _filmeRepository = new Mock<IFilmeRepository>();

            _generoFilmeServices = new GeneroFilmeServices(_mapper, _generoFilmeRepository.Object, _generoRepository.Object, _filmeRepository.Object);
        }

        //BUSCA
        [Fact]
        public async Task BuscarFilmesPorGenero_DadoGeneroInvalido_RetornaNull_GeneroNaoExiste()
        {
            //arrange 

            _generoFilmeRepository.Setup(genero => genero.BuscarFilmesPorGenero(1)).ReturnsAsync(null as IEnumerable<GeneroFilme>);


            //act
            var act = await _generoFilmeServices.BuscarFilmesPorGenero(1);
            //assert
            Assert.Null(act);

        }
        [Fact]
        public async Task BuscarFilmesPorGenero_DadoGeneroValido_RetornaFilmes()
        {
            //arrange
            var filme = new Filme() { IdFilme = 1, Titulo = "filme"};
            var genero = new Genero() { IdGenero = 1, NomeGenero = "Genero" };
            var generosFilmes = new List<GeneroFilme>() {
                    new GeneroFilme()
                    {

                        Filme = filme, Genero = genero, IdGenero = genero.IdGenero,
                        IdFilme = filme.IdFilme,  IdGeneroFilme = 1

                    }
                 };
            _generoFilmeRepository.Setup(g => g.BuscarFilmesPorGenero(genero.IdGenero)).ReturnsAsync(generosFilmes);
            var filmeDto = _mapper.Map<LerFilmeDto>(filme);
            var generoDto = _mapper.Map<LerGeneroDto>(genero);
            
            //act
            var generoService = await _generoFilmeServices.BuscarFilmesPorGenero(generoDto.IdGenero);
            var primeioFilmeDoGenero = generoService.FirstOrDefault(f=>f.FilmeDto.Titulo==filmeDto.Titulo);

            //assert
            Assert.Equal(filme.Titulo, primeioFilmeDoGenero.FilmeDto.Titulo);

        }

        //ADICIONA
        [Fact]
        public async Task AdicionarGeneroFilme_RetornaFalse_GeneroNaoExiste()
        {
            //arrange 

            _generoRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Genero);
            var generoDto = new CriarGeneroFilmeDto() { IdGenero = 1, IdFilme = 1 };
            
            //act
            var act = await _generoFilmeServices.AdicionarGeneroFilme(generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AdicionarGeneroFilme_RetornaFalse_FilmeNaoExiste()
        {
            //arrange 

            _filmeRepository.Setup(a => a.BuscarPorId(1)).ReturnsAsync(null as Filme);
            var generoDto = new CriarGeneroFilmeDto() { IdGenero = 1, IdFilme = 1 };

            //act
            var act = await _generoFilmeServices.AdicionarGeneroFilme(generoDto);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }



        //Alterar
        [Fact]
        public async Task AlterarGeneroDoFilme_RetornaFalse_GeneroNovoNaoExiste()
        {
            //arrange 
            _generoRepository.Setup(atoresFilme => atoresFilme.BuscarPorId(1)).ReturnsAsync(null as Genero);

            //act
            var act = await _generoFilmeServices.AlterarGeneroDoFilme(1, 1, 1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarGeneroDoFilme_RetornaFalse_GeneroAtualEFilmeNaoExistem()
        {
            //arrange 
          
          
            _generoFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarGeneroDoFilme(1, 1)).ReturnsAsync(null as GeneroFilme);
            //act
            var act = await _generoFilmeServices.AlterarGeneroDoFilme(6, 6,6);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task AlterarGeneroDoFilme_RetornaTrue_GeneronovoExisteEGeneroAtualEFilmeExistem()
        {
            //arrange 
            var generoNovo = new Genero() { IdGenero = 2, NomeGenero = "genero" };
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero2" };
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var generoesFilme = new GeneroFilme()
            {
                IdGenero = genero.IdGenero,
                IdFilme = filme.IdFilme,

            };
            //verifica Genero novo
            _generoRepository.Setup(atoresFilme => atoresFilme.BuscarPorId(2)).ReturnsAsync(generoNovo);
            //verificar se existe GeneroEFilme
            _generoFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarGeneroDoFilme(genero.IdGenero, filme.IdFilme)).ReturnsAsync(generoesFilme);

            //act
            var act = await _generoFilmeServices.AlterarGeneroDoFilme(genero.IdGenero, filme.IdFilme, generoNovo.IdGenero);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }


        //EXCLUIR
        [Fact]
        public async Task ExcluirGeneroDoFilme_RetornaFalse_GeneroEFilmeNaoExistem()
        {
            //arrange
            _generoFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarGeneroDoFilme(1, 1)).ReturnsAsync(null as GeneroFilme);

            //act
            var act = await _generoFilmeServices.DeletarGeneroDoFilme(1, 1);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.False(resultado);
        }
        [Fact]
        public async Task ExcluirGeneroDoFilme_RetornaTrue_GeneroEFilmeExistem()
        {

            //arrange
            var genero = new Genero() { IdGenero = 1, NomeGenero = "genero2" };
            var filme = new Filme() { IdFilme = 1, Titulo = "filme" };
            var generoesFilme = new GeneroFilme()
            {
                IdGenero = genero.IdGenero,
                IdFilme = filme.IdFilme,

            };
            _generoFilmeRepository.Setup(atoresFilme => atoresFilme.BuscarGeneroDoFilme(1, 1)).ReturnsAsync(generoesFilme);

            //act
            var act = await _generoFilmeServices.DeletarGeneroDoFilme(genero.IdGenero, filme.IdFilme);
            var resultado = TesteRepository.Retorna_FalseInFalid_TrueInSucess_Result(act);
            //assert
            Assert.True(resultado);
        }



    }
}
