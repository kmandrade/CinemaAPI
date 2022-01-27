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
        [Required]
        public string NomeGenero { get; set; }
    }
}
