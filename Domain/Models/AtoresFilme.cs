using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class AtoresFilme
    {
        [Key]
        public int IdAtoresFilme { get; set; }

        public virtual Filme Filme { get; set; }
        public int IdFilme { get; set; }

        public virtual Ator Ator { get; set; }
        public int IdAtor { get; set; }
    }
}
