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
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public Diretor Diretor { get; set; }
        public int DiretorId { get; set; }

      
        public virtual IEnumerable<Genero> Generos { get; set; }

       
        public virtual IEnumerable<Ator> Atores { get; set; }


        public SituacaoFilme Situacao { get; set; }


    }
}
