using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.DiretorDto
{
    public class LerDiretorDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NomeDiretor { get; set; }


        public virtual IEnumerable<Models.Filme> Filmes { get; set; }
    }
}
