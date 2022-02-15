using Domain.Dtos.VotosDto;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VotosController : ControllerBase
    {
        private IVotosService _votosService;
        public VotosController(IVotosService votosService)
        {
            _votosService = votosService;
        }
        [HttpPost("AdicionaVotoEmFilme")]
        public IActionResult AdicionaVotoEmFilme(AdicionaVotosDto votosDto)
        {
            _votosService.AdicionaVotosEmFilme(votosDto);
            return Ok();
        }
        [HttpGet("BuscaFilmesMaisVotados")]
        public IActionResult BuscaFilmesMaisVotados()
        {
            var filmes = _votosService.BuscaFilmesMaisVotados();
            return Ok(filmes);
        }
    }
}
