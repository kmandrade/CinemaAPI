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
        void CadastraDiretor(CriarDiretorDto DiretorDto);

        IEnumerable<LerDiretorDto> LerDiretores();

        
    }
}
