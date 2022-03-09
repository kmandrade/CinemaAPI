using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.InterfacesService;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("controller")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService service)
        {
            _filmeService = service;
        }
        
        [HttpGet("BuscarTodosFilmes")]
        public async Task<IActionResult> BuscarFilmes([FromQuery] int skip, int take)
        {
            if (skip <= 0 || take <= 0)
            {
                return BadRequest(new { message = "Paginacao Errada" });
            }
            var filmes = await _filmeService.BuscarTodos(skip,take);

            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound(new { message = "Nao foi possivel buscar os filmes" });
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscarFilmesArquivados")]
        public async Task<IActionResult> BuscarFilmesArquivados([FromQuery] int skip, int take)
        {
            if (skip <= 0 || take <= 0)
            {
                return BadRequest(new { message = "Paginacao Errada" });
            }

            var filmesArq= await _filmeService.BuscarFilmesArquivados(skip,take);
            if (filmesArq == null)
            {
                return BadRequest(new { message = "Nao foi possivel buscar os filmes" });
            }
            return Ok(filmesArq);
        }
        [HttpGet("BuscaCompleta/{id}")]
        public async Task<IActionResult> BuscarFilmeDetalgado(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var filme = await _filmeService.BuscarFilmeCompleto(id);
            if (filme == null)
            {
                return BadRequest(new { message = "Filme nao encontrado" });
            }
            return Ok(filme);
        }
        [HttpGet("BucaUmFilme/{id}")]
        public async Task<IActionResult> BuscarUmFilme(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var filme = await _filmeService.BuscarPorId(id);

            if (filme != null)
            {
                return Ok(filme);
            }
            return BadRequest(new { message = "Filme nao encontrado" });
        }

        [HttpGet("BuscarFilmesMaisVotados")]
        public async Task<IActionResult> BuscarFilmesMaisVotados()
        {
            
            var filmes = await _filmeService.BuscarFilmesMaisVotados();
            if (filmes == null)
            {
                return BadRequest(new { message = "Nao foi possivel encontrar os filmes" });
            }
            return Ok(filmes);
        }

        [Authorize(Roles ="Administrador")]
        [HttpPost("CadastrarUmFilme")]
        public async Task<IActionResult> CadastrarFilme([FromBody] CriarFilmeDto criarFilmeDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var resultado = await _filmeService.Cadastrar(criarFilmeDto);
            
            if (resultado.IsFailed)
            {
                return BadRequest(new {message= resultado.ToString() });
                
            }
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlterarUmFilme")]
        public async Task<IActionResult> AlterarFilme(int id, [FromBody] AlterarFilmeDto filmeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            Result resultado = await _filmeService.Alterar(id, filmeDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ArquivaUmFilme{id}")]
        public async Task<IActionResult> ArquivarFilme(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Filme nao existe" });
            }

            Result resultado= await _filmeService.ArquivarFilme(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("ReativarFilme{id}")]
        public async Task<IActionResult> ReativarFilme(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Filme nao existe" });
            }

            Result resultado = await _filmeService.ReativarFilme(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            await _filmeService.ReativarFilme(id);
            return Ok();
        }


        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaUmFilme/{id}")]
        public async Task<IActionResult> DeletaUmFilme(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Filme nao existe" });
            }
            Result resultado = await _filmeService.Excluir(id);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }
        


    }
}
