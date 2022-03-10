using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Votos
    {
        [Key]
        public int IdVotos { get; set; }

        public int ValorDoVoto { get; set; }

        public virtual Filme Filme { get; set; }
        public int IdFilme { get; set; }

        public virtual Usuario Usuario { get; set; }
        public int IdUsuario { get; set; }


    }
}