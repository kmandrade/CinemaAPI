using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.DiretorDto
{
    public class AlterarDiretorDto
    {

        [Required(ErrorMessage = "O nome do diretor é obrigatório")]
        public string NomeDiretor { get; set; }

    }
}
