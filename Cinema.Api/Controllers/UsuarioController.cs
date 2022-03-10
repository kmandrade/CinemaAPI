using Domain.Dtos.UsuarioDto;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscarTodosUsuariosComSenha")]
        public async Task<IActionResult> BuscarTodosUsuariosComSenha([FromQuery] int skip, int take)
        {
            if (skip <= 0 || take <= 0)
            {
                return BadRequest(new { message = "Paginacao Errada" });
            }
            var usuarios = await _usuarioService.BuscarTodosOsUsuarioDto(skip, take);
            if (usuarios == null)
            {
                return BadRequest(new { message = "Nao foi possivel encontrar usuarios" });
            }
            return Ok(usuarios);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscaUsuario/{id}")]
        public async Task<IActionResult> BuscarUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var usuario = await _usuarioService.BuscarUsuarioPorId(id);
            if (usuario.IsFailed)
            {
                return BadRequest(usuario.ToString());
            }
            return Ok(usuario.ValueOrDefault);
        }

        [HttpPost("RegistraUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _usuarioService.CriarUsuarioNormalDto(usuarioDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ArquivaUsuario/{id}")]
        public async Task<IActionResult> ArquivaUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            Result resultado = await _usuarioService.ArquivarUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok(resultado);

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ReativarUsuario/{id}")]
        public async Task<IActionResult> ReativarUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            Result resultado = await _usuarioService.ReativarUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok(new { message = "Usuario Ativado" });

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlterarUsuario")]
        public async Task<IActionResult> AlterarUsuario([FromQuery] int idUsuario, [FromBody] CriarUsuarioDto criarUsuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idUsuario <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado = await _usuarioService.AlterarUsuario(idUsuario, criarUsuarioDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaUsuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado = await _usuarioService.ExcluirUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }



    }
}
