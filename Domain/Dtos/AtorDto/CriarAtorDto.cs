using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.AtorDto
{
    public class CriarAtorDto
    {
        [Required(ErrorMessage = "Nome do ator é obrigatorio")]
        public string NomeAtor { get; set; }

    }
}
