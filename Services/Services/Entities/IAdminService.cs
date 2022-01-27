using Domain.Dtos.FilmeDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Entities
{
    public interface IAdminService
    {
        IEnumerable<LerFilmeDto> ConsultaFilmes();
        LerFilmeDto ConsultaFilmePorId(int id);


        void CadastraFilme(CriarFilmeDto filme);
        void ModificaFilme(AlterarFilmeDto filme);
        void RemoveFilme(Filme filme);


    }
}
