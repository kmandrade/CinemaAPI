using AutoMapper;
using Cinema.Api.Profiles;
using Data.Entities;
using Data.Services.Handlers;
using Domain.Dtos.FilmeDto;
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
using Xunit;

namespace Testes.Services
{
    public class TesteGeneroFilmeService
    {
        private readonly IMapper _mapper;
        private readonly GeneroFilmeServices _generoFilmeServices;
        private readonly GeneroServices _generoServices;
        private readonly FilmeServices _filmeServices;
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
        public async void BuscaFilmesPorGenero_DadoGeneroInvalido_RetornaNull_GeneroNaoExiste()
        {
            //arrange 

            _generoFilmeRepository.Setup(genero => genero.BuscaFilmesPorGenero(1)).ReturnsAsync(null as IEnumerable<GeneroFilme>);


            //act
            var act = await _generoFilmeServices.BuscaFilmesPorGenero(1);
            //assert
            Assert.Null(act);

        }
        [Fact]
        public async void BuscaFilmesPorGenero_DadoGeneroValido_RetornaFilmes()
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
            _generoFilmeRepository.Setup(g => g.BuscaFilmesPorGenero(genero.IdGenero)).ReturnsAsync(generosFilmes);
            var filmeDto = _mapper.Map<LerFilmeDto>(filme);
            var generoDto = _mapper.Map<LerGeneroDto>(genero);
            var generoesFilmesDto = _mapper.Map<IEnumerable<GeneroFilme>>(generosFilmes);
            //act
            var generoService = await _generoFilmeServices.BuscaFilmesPorGenero(generoDto.IdGenero);
            var primeioFilmeDoGenero = generoService.FirstOrDefault(f=>f.FilmeDto.Titulo==filmeDto.Titulo);

            //assert
            Assert.Equal(filme.Titulo, primeioFilmeDoGenero.FilmeDto.Titulo);

        }

        //ADICIONA


    }
}
