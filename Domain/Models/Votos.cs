using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Votos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ValorDoVoto { get; set; }


        [JsonIgnore]
        public virtual IEnumerable<Filme> Filmes { get; set; }
    }
}