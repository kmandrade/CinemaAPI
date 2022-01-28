using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class GeneroServices : IGeneroService
    {
        public void Cadastra(CriarGeneroDto obj)
        {
            throw new NotImplementedException();
        }

        public LerGeneroDto ConsultaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LerGeneroDto> ConsultaTodos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(LerGeneroDto genero)
        {
            throw new NotImplementedException();
        }

        public void Modifica(AlterarGeneroDto obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Genero obj)
        {
            throw new NotImplementedException();
        }
    }
}
