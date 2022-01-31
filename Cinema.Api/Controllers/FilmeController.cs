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

        [HttpPost]
        public IActionResult CadastraFilme([FromBody] CriarFilmeDto criarFilmeDto)
        {
            _filmeService.Cadastra(criarFilmeDto);
            return Ok();
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

    }
}
