using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.DiretorDto
{
    public class CriarDiretorDto
    {
        [Required]
        public string NomeDiretor { get; set; }


    }
}
