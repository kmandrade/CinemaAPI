using AutoMapper;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;
using Servicos.Services.Token;

namespace Cinema.Api.Controllers
{

    [ApiController]
    [Route("Controller")]
    public class LoginController :ControllerBase
    {
        private ITokenService _tokenService;
        private IUsuarioService _usuarioService;
        public LoginController(ITokenService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginRequest request)
        {
            //recupera o usuario
            var usuario = _usuarioService.BuscaUsuarioPorLogin(request);
            
           //verifica se usuario existe
            if (usuario == null || usuario.Situacao==SituacaoEntities.Arquivado)
            {
                return NotFound(new { message = "Usuario ou senha invalidos" });
            }

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
}   }

