using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class GeneroFilme
    {
        [Key]
        public int IdGeneroFilme { get; set; }

        public Filme Filme { get; set; }
        public int IdFilme { get; set; }

        public Genero Genero { get; set; }
        public int IdGenero { get; set; }
    }
}
