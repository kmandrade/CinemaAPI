using AutoMapper;
using Cinema.Api.Profiles;
using Data.Entities;
using Data.Services.Handlers;
using Domain.Profiles;
using Moq;
using Servicos.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Services
{
    public class TesteAtorFilmeService
    {
        private readonly IMapper _mapper;
        private readonly AtorFilmeServices _atorFilmeServices;
        private readonly AtorServices _atorServices;
        private readonly FilmeServices _filmeServices; 
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

            _atorFilmeServices = new AtorFilmeServices(_mapper, _atorFilmeRepository.Object, _atorRepository.Object);
        }





    }
}
