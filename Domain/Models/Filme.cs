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
        //public Filme()
        //{
        //    Atores = new HashSet<Ator>();
        //}


        [Key]
        [Required]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        public int Duracao { get; set; }

        public virtual Diretor Diretor { get; set; }
        public int DiretorId { get; set; }


        public List<AtoresFilme> Atores { get; set; }
        public List<GeneroFilme> Generos { get; set; }

        
        


        
        [Range (1,4,ErrorMessage ="O Voto so pode ser de 1 como ruim a 4 como otimo")]
        public virtual IEnumerable<Votos> Votos { get; set; }

        public SituacaoFilme Situacao { get; set; }

        
    }
}
