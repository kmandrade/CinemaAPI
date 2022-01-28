using Domain.Dtos.DiretorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IDiretorService
    {
        IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(LerDiretorDto diretorDto);

        IEnumerable<LerDiretorDto> ConsultaTodos();
        LerDiretorDto ConsultaPorId(int id);


        void Cadastra(CriarDiretorDto obj);
        void Modifica(AlterarDiretorDto obj);
        void Remove(Diretor obj);
    }
}
