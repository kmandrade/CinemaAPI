using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AtorDto
{
    public class CriarAtorDto
    {
        [Required]
        public string NomeAtor { get; set; }

    }
}
