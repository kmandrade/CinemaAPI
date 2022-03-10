using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Diretor
    {
        //Um diretor pode ter varios filmes  relacao de 1 para N
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NomeDiretor { get; set; }


        public virtual IEnumerable<Filme> Filmes { get; set; }


    }
}
