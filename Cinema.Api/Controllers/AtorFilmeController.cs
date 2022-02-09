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
        [HttpGet("LerFilmesPorAtor")]
        public IActionResult LerFilmesPorAtor([FromQuery] int idAtor)
        {
            var filmes = _atorFilmeService.BuscaFilmesPorAtor(idAtor);
            return Ok(filmes);
        }

        [HttpPost("AdicionaAtorFilme")]
        public IActionResult AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            _atorFilmeService.AdicionaAtorFilme(criarAtorFilmeDto);
            return Ok();
        }
        

    }
}
