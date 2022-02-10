using Domain.Dtos.FilmeDto;
using Domain.Models;
using Domain.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Controllers
{
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
        public IActionResult BuscaFilmes()
        {
            var filmes = _filmeService.ConsultaTodos();

            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }
        [HttpGet("BuscaCompleta/{id}")]
        public IActionResult BuscaCompleta(int id)
        {
            var filme = _filmeService.BuscarFilmeCompleto(id);
            return Ok(filme);
        }
        [HttpGet("BucaUmFilme/{id}")]
        public IActionResult BuscaUmFilme(int id)
        {
            var filme = _filmeService.ConsultaPorId(id);

            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

        [HttpPost("CadastraUmFilme")]
        public IActionResult CadastraFilme([FromBody] CriarFilmeDto criarFilmeDto)
        {
            _filmeService.Cadastra(criarFilmeDto);
            return Ok();
        }

        [HttpDelete("DeletaUmFilme/{id}")]
        public IActionResult DeletaUmFilme(int id)
        {
            _filmeService.Excluir(id);
            return Ok();
        }
       
        [HttpPut("AlteraUmFilme")]
        public IActionResult AlterarFilme(int id,[FromBody]AlterarFilmeDto filmeDto)
        {
            _filmeService.Altera(id,filmeDto);
            return NoContent();
        }
        [HttpPut("ArquivaUmFilme{id}")]
        public IActionResult ArquivarFilme(int id)
        {
            _filmeService.ArquivarFilme(id);
            return NoContent();
        }
        [HttpPut("ReativarFilme{id}")]
        public IActionResult ReativarFilme(int id)
        {
            _filmeService.ReativarFilme(id);
            return Ok();
        }


    }
}
