using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.FilmeDto
{
    public class AlterarFilmeDto
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        [Range(5, 30, ErrorMessage = "O nome do filme pode ser de 5 a 30 caracteres")]
        public string Titulo { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 minuto e no máximo 600.")]
        public int Duracao { get; set; }

        public int IdGenero { get; set; }
        public int IdAtor { get; set; }


        public SituacaoFilme Situacao { get; set; }

    }
}
