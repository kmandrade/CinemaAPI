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
       
        [HttpGet("BuscaFilmesMaisVotados")]
        public IActionResult BuscaFilmesMaisVotados()
        {
            var filmes = _votosService.BuscaFilmesMaisVotados();
            return Ok(filmes);
        }

        [HttpPost("AdicionaVotoEmFilme")]
        public IActionResult AdicionaVotoEmFilme([FromBody]AdicionaVotosDto votosDto)
        {
            var Id = User.Claims.First(u => u.Type == "Id").Value;
            _votosService.AdicionaVotosEmFilme(votosDto, int.Parse(Id));
            return Ok();
        }


        [HttpPut("AlteraValorDoVotoEmFilme")]
        public IActionResult AlteraVotoEmFilme ([FromQuery]int idVoto, int valorDoVoto)
        {
            _votosService.AlteraValorDoVotoEmFilme(idVoto, valorDoVoto); 
            return Ok();
        }


        [HttpDelete("DeletaVotoEmFilme/{id}")]
        public IActionResult DeletaVotoEmFilme(int id)
        {
            _votosService.RemoverVoto(id);
            return Ok();
        }

    }
}
