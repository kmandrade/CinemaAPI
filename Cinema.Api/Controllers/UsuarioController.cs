using Domain.Dtos.UsuarioDto;
using FluentResults;
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

        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscaTodosUsuariosComSenha")]
        public async Task<IActionResult> BuscaTodosUsuariosComSenha([FromQuery] int skip, int take)
        {
            var usuarios = await _usuarioService.BuscaTodosOsUsuarioDto(skip,take);
            return Ok(usuarios);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscaUsuario/{id}")]
        public async Task<IActionResult> BuscaUsuario(int id)
        {
            var usuario = await _usuarioService.BuscaUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpPost("RegistraUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody]CriarUsuarioDto usuarioDto)
        {
            await _usuarioService.CriarUsuarioNormalDto(usuarioDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ArquivaUsuario/{id}")]
        public async Task<IActionResult> ArquivaUsuario(int id)
        {
            Result resultado = await _usuarioService.ArquivarUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = "Usuario nao existe ou ja arquivado" });
            }
            return Ok(resultado);
          
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ReativarUsuario/{id}")]
        public async Task<IActionResult> ReativarUsuario(int id)
        {
            Result resultado = await _usuarioService.ReativarUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = "Usuario nao existe ou ja ativado" });
            }
            return Ok(new { message = "Usuario Ativado" });

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraUsuario")]
        public async Task<IActionResult> AlteraUsuario([FromQuery]int idUsuario,[FromBody] CriarUsuarioDto criarUsuarioDto)
        {
            await _usuarioService.AlteraUsuario( idUsuario, criarUsuarioDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaUsuario/{id}")]
        public async Task<IActionResult> DeletaUsuario(int id)
        {
            await _usuarioService.DeletaUsuario(id);
            return Ok();
        }



    }
}
