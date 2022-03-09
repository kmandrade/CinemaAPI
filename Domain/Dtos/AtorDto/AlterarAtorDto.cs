using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AtorDto
{
    public class AlterarAtorDto
    {
        [Required(ErrorMessage ="O nome do ator é obrigatório")]
        public string NomeAtor { get; set; }
    }
}
