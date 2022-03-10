using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Genero
    {   //varios generos para varios filmes
        [Key]
        [Required]
        public int IdGenero { get; set; }

        public string NomeGenero { get; set; }

        public virtual List<GeneroFilme> GeneroFilmes { get; set; }

    }
}
