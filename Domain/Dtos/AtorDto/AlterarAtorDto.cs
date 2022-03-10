using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.AtorDto
{
    public class AlterarAtorDto
    {
        [Required(ErrorMessage = "O nome do ator é obrigatório")]
        public string NomeAtor { get; set; }
    }
}
