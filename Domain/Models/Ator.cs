using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ator
    {

        [Key]
        [Required]
        public int IdAtor { get; set; }
        [Required]
        public string NomeAtor { get; set; }

        
        public virtual ICollection<Filme> Filmes { get; set; }
        public List<AtorFilme> AtorFilmes { get; set; }


    }
}
