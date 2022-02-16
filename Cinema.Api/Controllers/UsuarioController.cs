using Domain.Dtos.UsuarioDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class UsuarioController: ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CriarUsuario(CriarUsuarioDto usuarioDto)
        {
            _usuarioService.CriarUsuarioNormalDto(usuarioDto);
            return Ok();
        }

    }
}
