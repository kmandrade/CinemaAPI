using Domain.Dtos.AtorFilme;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Handlers;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtorFilmeController : ControllerBase

    {
        private AtorFilmeServices _atorFilmeService;

        public AtorFilmeController([FromBody]AtorFilmeServices atorFilmeService)
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
        public IActionResult LerFilmesPorAtor(LerAtorFilmeDto lerAtorFilmeDto)
        {
            var filmes = _atorFilmeService.BuscaFilmesPorAtor(lerAtorFilmeDto);
            return Ok(filmes);
        }

    }
}
