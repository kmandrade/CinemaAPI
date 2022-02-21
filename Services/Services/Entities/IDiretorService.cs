using Domain.Dtos.DiretorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Entities
{
    public interface IDiretorService
    {
        IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(int idDiretor);

        IEnumerable<LerDiretorDto> ConsultaTodos(int skip, int take);
        LerDiretorDto ConsultaPorId(int id);

        void Cadastra(CriarDiretorDto obj);
        void Altera(int id, AlterarDiretorDto obj);
        void Excluir(int id);
    }
}
