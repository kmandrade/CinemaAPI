using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class AtorServices : IAtorService
    {


        public void Cadastra(CriarAtorDto obj)
        {
            throw new NotImplementedException();
        }

        public LerAtorDto ConsultaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LerAtorDto> ConsultaTodos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(LerAtorDto ator)
        {
            throw new NotImplementedException();
        }

        public void Modifica(AlterarAtorDto obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Ator obj)
        {
            throw new NotImplementedException();
        }
    }
}
