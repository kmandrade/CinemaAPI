using Domain.Dtos.VotosDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VotosController : ControllerBase
    {
        private readonly IVotosService _votosService;
        public VotosController(IVotosService votosService)
        {
            _votosService = votosService;
        }
       
        

        [HttpPost("AdicionaVotoEmFilme")]
        public async Task<IActionResult> AdicionaVotoEmFilme([FromBody]AdicionaVotosDto votosDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado= await _votosService.AdicionarVotosEmFilme(votosDto, BuscaIdUsuarioPorJWT());
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }


        [HttpPut("AlterarValorDoVotoEmFilme")]
        public async Task<IActionResult> AlterarVotoEmFilme ([FromQuery] int idFilme,int valorDoVoto)
        {
            if (idFilme <= 0 || valorDoVoto < 0 || valorDoVoto>4 )
            {
                return BadRequest(new { message = "O id precisa ser maior que 0 e o voto precisa ser de 0 a 4" });
            }
            var resultado= await _votosService.AlterarValorDoVotoEmFilme(idFilme, valorDoVoto, BuscaIdUsuarioPorJWT());
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        [HttpDelete("DeletaVotoEmFilme")]
        public async Task<IActionResult> ExcluirVotoDoFilme([FromQuery]int idFilme)
        {
            if (idFilme <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado= await _votosService.ExcluirVotoDoFilme(idFilme, BuscaIdUsuarioPorJWT());
             if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        private int BuscaIdUsuarioPorJWT()
        {
            var idUsuario = User.Claims.First(u => u.Type == "Id").Value;
            return int.Parse(idUsuario);
        }

    }
}
