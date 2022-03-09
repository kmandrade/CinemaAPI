using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Filme
    {
      

        [Key]
        [Required]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        public int Duracao { get; set; }

        public virtual Diretor Diretor { get; set; }
        public int DiretorId { get; set; }

        public int TotalDeVotos { get; set; }
        public virtual List<AtoresFilme> AtoresFilme { get; set; }

        public virtual List<GeneroFilme> GenerosFilme { get; set; }

        
        [Range (0,4,ErrorMessage ="O Voto so pode ser de 0 como ruim a 4 como otimo")]
        public virtual List<Votos> Votos { get; set; }

        public SituacaoEntities Situacao { get; set; }

       
    }
}
