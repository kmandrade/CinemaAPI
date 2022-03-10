using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.DiretorDto
{
    public class CriarDiretorDto
    {
        [Required(ErrorMessage = "O nome do diretor é obrigatório")]
        public string NomeDiretor { get; set; }


    }
}
