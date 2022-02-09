using Domain.Dtos.FilmeDto;
using Domain.Dtos.FilmeGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IGeneroFilmeService
    {
        void AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto);
        IEnumerable<LerGeneroFilmeDto> BuscarFilmesPorGenero(int IdGeneroFilme);
        void DeletaGeneroDoFilme(int idGenero, int idFilme);        
    }
}
