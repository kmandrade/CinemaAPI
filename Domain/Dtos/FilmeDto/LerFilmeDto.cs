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

        public virtual Diretor Diretor { get; set; }
        public int DiretorId { get; set; }


        public virtual List<AtoresFilme> AtoresFilme { get; set; }


        public virtual List<GeneroFilme> GenerosFilme { get; set; }


        public virtual IEnumerable<Votos> ?Votos { get; set; }

        public SituacaoFilme Situacao { get; set; }
    }
}
