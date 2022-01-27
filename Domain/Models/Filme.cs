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
        [Range(5,30,ErrorMessage ="O nome do filme pode ser de 5 a 30 caracteres")]
        [Required(ErrorMessage ="O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 minuto e no máximo 600.")]
        public int Duracao { get; set; }


        public virtual Diretor Diretor { get; set; }
        public int DiretorId { get; set; }


        
        public virtual IEnumerable<Genero> Generos { get; set; }

       
        public virtual IEnumerable<Ator> Atores { get; set; }

        
        [Range (1,4,ErrorMessage ="O Voto so pode ser de 1 como ruim a 4 como otimo")]
        public virtual IEnumerable<Votos> Votos { get; set; }

        public SituacaoFilme Situacao { get; set; }


    }
}
