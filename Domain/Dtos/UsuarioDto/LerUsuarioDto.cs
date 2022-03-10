using Domain.Models;

namespace Domain.Dtos.UsuarioDto
{
    public class LerUsuarioDto
    {
        public int IdUsuario { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public SituacaoEntities Situacao { get; set; }
    }
}
