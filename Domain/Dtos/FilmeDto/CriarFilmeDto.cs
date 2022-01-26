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
    public class CriarFilmeDto
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 minuto e no máximo 600.")]
        public int Duracao { get; set; }

        

        public SituacaoFilme Situacao { get; set; }

    }
}
