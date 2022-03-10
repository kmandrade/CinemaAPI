using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.FilmeDto
{
    public class CriarFilmeDto
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo Duracao é obrigatorio")]
        public int Duracao { get; set; }
        [Required(ErrorMessage = "O campo IdDiretor é obrigatorio")]
        public int DiretorId { get; set; }




    }

}
