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
        public int Id { get; set; }

        public string TipoGenero { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Filme> Filmes { get; set; }

    }
}
