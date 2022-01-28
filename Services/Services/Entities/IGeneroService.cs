using Domain.Dtos.FilmeDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IGeneroService
    {
        IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(LerGeneroDto genero);

        IEnumerable<LerGeneroDto> ConsultaTodos();
        LerGeneroDto ConsultaPorId(int id);


        void Cadastra(CriarGeneroDto obj);
        void Modifica(AlterarGeneroDto obj);
        void Remove(Genero obj);
    }
}
