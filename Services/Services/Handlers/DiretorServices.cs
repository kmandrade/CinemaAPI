using AutoMapper;
using Data.Entities;
using Domain.Dtos.DiretorDto;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class DiretorServices : IDiretorService
    {
        
        private readonly IMapper _mapper;
        public DiretorServices(IRepository repository, IMapper mapper )
        {
            _mapper=mapper;
            _repository = repository;

        }

        public void CadastraDiretor(CriarDiretorDto DiretorDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LerDiretorDto> LerDiretores()
        {
            throw new NotImplementedException();
        }
    }
}
