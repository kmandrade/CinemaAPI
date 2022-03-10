using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.GeneroDto
{
    public class AlterarGeneroDto
    {
        [Required(ErrorMessage = "O nome do genero é obrigatório")]
        public string NomeGenero { get; set; }
    }
}
