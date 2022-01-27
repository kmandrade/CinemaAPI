using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.GeneroDto
{
    public class LerGeneroDto
    {
        [Key]
        [Required]
        public int IdGenero { get; set; }

        public string TipoGenero { get; set; }


        public virtual IEnumerable<Filme> Filmes { get; set; }

    }
}
