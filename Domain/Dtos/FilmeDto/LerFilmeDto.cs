using Domain.Dtos.AtorDto;
using Domain.Dtos.DiretorDto;
using Domain.Dtos.GeneroDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Dtos.FilmeDto
{
    public class LerFilmeDto
    {
        public int IdFilme { get; set; }

        
        public string Titulo { get; set; }
        public int Duracao { get; set; }

        public LerDiretorDto DiretorDto { get; set; }
        //passei para o automapper FilmeProfile que 
        //O meu LerFilmeDto que tem como atributo LerAtorDto
        // esse atributo (LerAtorDto) seria mapeado para a entidade de Filme
        // para o campo AtoresFilmes 
        // Em seguida em AtorFilmeProfile 
        // mapeei AtoresFilmes para LerAtorDto o campo nomeAtor
        public List<LerAtorDto> AtoresDto { get; set; }

        public  List<LerGeneroDto> GenerosDto { get; set; }


        public virtual IEnumerable<Votos> Votos { get; set; }

        public SituacaoFilme Situacao { get; set; }
    }
}
