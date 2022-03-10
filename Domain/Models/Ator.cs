using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Ator
    {

        [Key]
        [Required]
        public int IdAtor { get; set; }
        [Required]
        public string NomeAtor { get; set; }


        public virtual List<AtoresFilme> AtoresFilme { get; set; }


    }
}
