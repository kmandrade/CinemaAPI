using Domain.Dtos.VotosDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [Authorize]
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
            var Id = User.Claims.First(u => u.Type == "Id").Value;
            _votosService.AdicionaVotosEmFilme(votosDto,int.Parse(Id));
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
