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
        [Key]
        [Required]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 minuto e no máximo 600.")]
        public int Duracao { get; set; }


        public Diretor Diretor { get; set; }

        public int DiretorId { get; set; }

        public virtual IEnumerable<Votos> Votos { get; set; }

        public virtual IEnumerable<Genero> Generos { get; set; }

        
        public virtual IEnumerable<Ator> Atores { get; set; }


        public SituacaoFilme Situacao { get; set; }
    }
}
