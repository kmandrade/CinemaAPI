using AutoMapper;
using Data.Entities;
using Data.Services.Handlers;
using Domain.Profiles;
using Domain.Services.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
