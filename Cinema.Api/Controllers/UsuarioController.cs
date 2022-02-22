﻿using Domain.Dtos.UsuarioDto;
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
        public IActionResult BuscaTodosUsuariosComSenha([FromQuery] int skip, int take)
        {
            var usuarios = _usuarioService.BuscaTodosOsUsuarioDto(skip,take);
            return Ok(usuarios);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscaUsuario/{id}")]
        public IActionResult BuscaUsuario(int id)
        {
            var usuario = _usuarioService.BuscaUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpPost("RegistraUsuario")]
        [AllowAnonymous]
        public IActionResult CriarUsuario([FromBody]CriarUsuarioDto usuarioDto)
        {
            _usuarioService.CriarUsuarioNormalDto(usuarioDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ArquivaUsuario/{id}")]
        public IActionResult ArquivaUsuario(int id)
        {
            Result resultado = _usuarioService.ArquivarUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = "Usuario nao existe ou ja arquivado" });
            }
            return Ok(resultado);
          
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ReativarUsuario/{id}")]
        public IActionResult ReativarUsuario(int id)
        {
            Result resultado = _usuarioService.ReativarUsuario(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = "Usuario nao existe ou ja ativado" });
            }
            return Ok(new { message = "Usuario Ativado" });

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraUsuario")]
        public IActionResult AlteraUsuario([FromQuery]int idUsuario,[FromBody] CriarUsuarioDto criarUsuarioDto)
        {
            _usuarioService.AlteraUsuario( idUsuario, criarUsuarioDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaUsuario/{id}")]
        public IActionResult DeletaUsuario(int id)
        {
            _usuarioService.DeletaUsuario(id);
            return Ok();
        }



    }
}
