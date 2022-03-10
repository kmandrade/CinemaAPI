using System.ComponentModel.DataAnnotations;

namespace Domain.Requisicao
{
    public class RequisicaoLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
