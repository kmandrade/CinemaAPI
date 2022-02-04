using Domain.Dtos.AtorFilme;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Entities;
using Serviços.Services.Handlers;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtorFilmeController : ControllerBase

    {
        private IAtorFilmeService _atorFilmeService;

        public AtorFilmeController(IAtorFilmeService atorFilmeService)
        {
            _atorFilmeService = atorFilmeService;
        }

       
        [HttpPost("Adiciona AtorFilme")]
        public IActionResult AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            _atorFilmeService.AdicionaAtorFilme(criarAtorFilmeDto);
            return Ok();
        }
        [HttpGet("LerFilmesPorAtor")]
        public IActionResult LerFilmesPorAtor([FromQuery]int lerAtorFilmeDto)
        {
            var filmes = _atorFilmeService.BuscaFilmesPorAtor(lerAtorFilmeDto);
            return Ok(filmes);
        }
        [HttpGet("Busca Todos")]
        public IActionResult BuscaTodosAtoresFilmes()
        {
            var at = _atorFilmeService.BuscaTodosAtoresFilmes();
            return Ok(at);
        }

    }
}
