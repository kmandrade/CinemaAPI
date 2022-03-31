using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{

    [ApiController]
    [Route("Controller")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;
        public LoginController(ITokenService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginRequest request)
        {
            
            
            var valido = await _usuarioService.ValidaSenhaAsync(request);
            if (valido)
            {
                var usuario= await _usuarioService.BuscarUsuarioPorLogin(request.UserName);
                //gera o token
                var token = _tokenService.GenerateToken(usuario);
                //oculta a senha
                usuario.Password = "";

                //retorna os dados
                return new
                {
                    usuario.NomeUsuario,
                    token = token
                };
            }
            return BadRequest(new { message = "Usuario ou senha invalidos" });
            
        }
    }
}

