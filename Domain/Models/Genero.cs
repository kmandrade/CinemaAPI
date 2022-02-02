using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Genero
    {   //varios generos para varios filmes
        [Key]
        [Required]
        public int IdGenero { get; set; }

        public string NomeGenero { get; set; }

        
        public virtual IEnumerable<Filme> Filmes { get; set; }

    }   
}
