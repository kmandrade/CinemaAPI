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
        public async Task<IActionResult> BuscaFilmesMaisVotados([FromQuery] int skip, int take)
        {
            var filmes = await _votosService.BuscaFilmesMaisVotados(skip,take);
            return Ok(filmes);
        }

        [HttpPost("AdicionaVotoEmFilme")]
        public async Task<IActionResult> AdicionaVotoEmFilme([FromBody]AdicionaVotosDto votosDto)
        {
            await _votosService.AdicionaVotosEmFilme(votosDto, BuscaIdUsuarioPorJWT());
            return Ok();
        }


        [HttpPut("AlteraValorDoVotoEmFilme")]
        public async Task<IActionResult> AlteraVotoEmFilme ([FromQuery] int idVoto,int valorDoVoto)
        {
            
            await _votosService.AlteraValorDoVotoEmFilme(idVoto, valorDoVoto, BuscaIdUsuarioPorJWT()); 
            return Ok();
        }

        [HttpDelete("DeletaVotoEmFilme")]
        public async Task<IActionResult> DeletaVotoEmFilme([FromQuery]int idVoto)
        {
            
           await _votosService.RemoverVoto(idVoto, BuscaIdUsuarioPorJWT());
            return Ok();
        }

        private int BuscaIdUsuarioPorJWT()
        {
            var idUsuario = User.Claims.First(u => u.Type == "Id").Value;
            return int.Parse(idUsuario);
        }

    }
}
