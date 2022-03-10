using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.GeneroDto
{
    public class CriarGeneroDto
    {
        [Required(ErrorMessage = "O nome do genero é obrigatório")]
        public string NomeGenero { get; set; }
    }
}
