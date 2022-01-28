using Domain.Dtos.AtorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IAtorService
    {
        IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(LerAtorDto ator);

        IEnumerable<LerAtorDto> ConsultaTodos();
        LerAtorDto ConsultaPorId(int id);


        void Cadastra(CriarAtorDto obj);
        void Modifica(AlterarAtorDto obj);
        void Remove(Ator obj);


    }
}
