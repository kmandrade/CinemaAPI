using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.Entities;
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
        IFilmeService _filmeService;

        public FilmeController(IFilmeService service)
        {
            _filmeService = service;
        }
        
        [HttpGet("BuscaTodosFilmes")]
        public async Task<IActionResult> BuscaFilmes([FromQuery] int skip, int take)
        {
            var filmes = await _filmeService.BuscaTodos(skip,take);

            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscaFilmesArquivados")]
        public async Task<IActionResult> BuscaFilmesArquivados([FromQuery] int skip, int take)
        {
           var filmesArq= await _filmeService.BuscaFilmesArquivados(skip,take);
            return Ok(filmesArq);
        }
        [HttpGet("BuscaCompleta/{id}")]
        public async Task<IActionResult> BuscaCompleta(int id)
        {
            var filme = await _filmeService.BuscaFilmeCompleto(id);
            return Ok(filme);
        }
        [HttpGet("BucaUmFilme/{id}")]
        public async Task<IActionResult> BuscaUmFilme(int id)
        {
            var filme = await _filmeService.ConsultaPorId(id);

            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }
        [Authorize(Roles ="Administrador")]
        [HttpPost("CadastraUmFilme")]
        public async Task<IActionResult> CadastraFilme([FromBody] CriarFilmeDto criarFilmeDto)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            */
            await _filmeService.Cadastra(criarFilmeDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraUmFilme")]
        public async Task<IActionResult> AlterarFilme(int id, [FromBody] AlterarFilmeDto filmeDto)
        {
            Result resultado = await _filmeService.Altera(id, filmeDto);
            if (resultado.IsFailed)
            {
                return NotFound();
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
                return BadRequest(new { message = "Filme nao existe" });
            }
            await _filmeService.ArquivarFilme(id);
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
                return BadRequest(new { message = "Filme nao existe" });
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
                return BadRequest(new { message = "Filme nao existe" });
            }
            return Ok();
        }
        


    }
}
