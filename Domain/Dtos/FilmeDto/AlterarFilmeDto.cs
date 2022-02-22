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
        public string Titulo { get; set; }

        public int Duracao { get; set; }
        [Required]
        public int idDiretor { get; set; }


        public SituacaoEntities Situacao { get; set; }

    }
}
