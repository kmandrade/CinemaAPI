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

        public  LerDiretorDto Diretor { get; set; }
    
        public  List<LerAtorDto> Atores { get; set; }

        public  List<LerGeneroDto> Generos { get; set; }


       // public virtual IEnumerable<Votos> Votos { get; set; }

        public SituacaoFilme Situacao { get; set; }
    }
}
