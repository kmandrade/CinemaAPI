using AutoMapper;
using Data.Entities;
using Data.Services.Handlers;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Profiles;
using Domain.Services.Entities;
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
        public void BuscaFilmePorId_RetornaNullFilmeInexistente()
        {

            //Arranje
            int id = 2;
            ///_filmeDao.Setup(f => f.BuscarPorId(id)).Returns(null as Filme);
            //Act
            var resultadoService=_filmeService.ConsultaPorId(id);            
            //Assert
            //Assert.Equal(null, resultadoService);
        }
        [Theory]
        [InlineData(1)]
        public void BuscaFilmePorId_RetornaFilmeArquivado(int id)
        {
            //Arranje
            var filme = new Filme()
            {
               
            };
            //Act
            
            //Assert

        }

    }
}
