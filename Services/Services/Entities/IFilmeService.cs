using Domain.Dtos.FilmeDto;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Entities
{
    public interface IFilmeService
    {
        IEnumerable<LerFilmeDto> ConsultaTodos();
        LerFilmeDto ConsultaPorId(int id);


        void Cadastra(CriarFilmeDto obj);
        void Altera(AlterarFilmeDto obj);
        void Remove(Filme obj);

    }
}
