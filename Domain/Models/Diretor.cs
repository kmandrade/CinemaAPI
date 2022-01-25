using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Diretor
    {
        //Um diretor pode ter varios filmes  relacao de 1 para N
        [Key]
        [Required]
        public int Id { get; set; }

        public string NomeDiretor { get; set; }

     
        public virtual IEnumerable<Filme> Filmes { get; set; }


    }
}
