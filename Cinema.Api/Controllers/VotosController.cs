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
        public IActionResult BuscaFilmesMaisVotados([FromQuery] int skip, int take)
        {
            var filmes = _votosService.BuscaFilmesMaisVotados(skip,take);
            return Ok(filmes);
        }

        [HttpPost("AdicionaVotoEmFilme")]
        public IActionResult AdicionaVotoEmFilme([FromBody]AdicionaVotosDto votosDto)
        {
            _votosService.AdicionaVotosEmFilme(votosDto, BuscaIdUsuarioPorJWT());
            return Ok();
        }


        [HttpPut("AlteraValorDoVotoEmFilme")]
        public IActionResult AlteraVotoEmFilme ([FromQuery] int idVoto,int valorDoVoto)
        {
            
            _votosService.AlteraValorDoVotoEmFilme(idVoto, valorDoVoto, BuscaIdUsuarioPorJWT()); 
            return Ok();
        }

        [HttpDelete("DeletaVotoEmFilme")]
        public IActionResult DeletaVotoEmFilme([FromQuery]int idVoto)
        {
            
            _votosService.RemoverVoto(idVoto, BuscaIdUsuarioPorJWT());
            return Ok();
        }

        private int BuscaIdUsuarioPorJWT()
        {
            var idUsuario = User.Claims.First(u => u.Type == "Id").Value;
            return int.Parse(idUsuario);
        }

    }
}
