using AutoMapper;
using Data.Entities;
using Data.Services.Handlers;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Profiles;
using Domain.Services.Entities;
using FluentResults;
using Moq;
using Xunit;

namespace Testes.Services
{
    public class TesteFilmeService
    {
        private readonly IMapper _mapper;
        private readonly FilmeServices _filmeService;
        private readonly Mock<IFilmeDao> _filmeDao;

        public TesteFilmeService()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FilmeProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _filmeDao = new Mock<IFilmeDao>();
            _filmeService = new FilmeServices(_filmeDao.Object, _mapper);
        }

        [Fact]
        public  async void BuscaFilmePorId_RetornaNullFilmeInexistente()
        {

            //Arranje
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
            //Arranje
             
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
            var act = _filmeService.ConsultaPorId(id);
            //Assert
            Assert.Null(act.Result);
        }

    }
}
