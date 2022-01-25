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
        IEnumerable<Filme> ConsultaFilmes();
        Filme ConsultaFilmePorId(int id);
        //IEnumerable<Filme> ConsultaFilmesPorDiretor(string nomeDiretor);
        //IEnumerable<Filme> ConsultaFilmesPorAutor(string nomeAutor);
        //IEnumerable<Filme> ConsultaFilmesPorGenero(string nomeGenero);
        
        void CadastraFilme(Filme filme);
        void ModificaFilme(Filme filme);
        void RemoveFilme(Filme filme);


    }
}
