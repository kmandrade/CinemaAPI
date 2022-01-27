using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Votos
    {
        [Key]
        public int IdVotos { get; set; }

        [Required]
        public int ValorDoVoto { get; set; }

        public virtual Filme Filme { get; set; }
        public int IdFilme { get; set; }


        //ID USUARIO RELACIONAR FILME COM USUARIO É A CLASSE VOTO
    }
}