using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.GeneroDto
{
    public class AlterarGeneroDto
    {
        [Required(ErrorMessage = "O nome do genero é obrigatório")]
        public string NomeGenero { get; set; }
    }
}
