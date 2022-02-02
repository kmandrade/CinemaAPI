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
        [HttpGet("Buca Um Filme{id}")]
        public IActionResult BuscaUmFilme(int id)
        {
            var filme = _filmeService.ConsultaPorId(id);

            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }


        [HttpPost("CadastraFilmes")]
        public IActionResult CadastraFilme([FromBody] CriarFilmeDto criarFilmeDto)
        {
            _filmeService.Cadastra(criarFilmeDto);
            return Ok();
        }

        
        [HttpDelete("Deletar Um Filme")]
        public IActionResult DeletaUmFilme([FromQuery] int idFilme)
        {
            _filmeService.Excluir(idFilme);
            return NoContent();
        }
        [HttpPut("Arquiva Um Filme{id}")]
        public IActionResult ArquivarFilme(int id)
        {
            _filmeService.ArquivarFilme(id);
            return NoContent();
        }

        [HttpPut("Altera Um Filme")]
        public IActionResult AlterarFilme(int id,[FromBody]AlterarFilmeDto filmeDto)
        {
            _filmeService.Altera(id,filmeDto);
            return NoContent();
        }

    }
}
