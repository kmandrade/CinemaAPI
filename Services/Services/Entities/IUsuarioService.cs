using Domain.Dtos.UsuarioDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Entities
{
    public interface IUsuarioService
    {
        IEnumerable<LerUsuarioDto> LerTodosOsUsuarioDto();
        void CriarUsuarioDto(CriarUsuarioDto criarUsuarioDto);

    }
}
