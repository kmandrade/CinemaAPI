using Domain.Dtos.AtorFilme;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AtorFilmeController : ControllerBase

    {
        private readonly IAtorFilmeService _atorFilmeService;

        public AtorFilmeController(IAtorFilmeService atorFilmeService)
        {
            _atorFilmeService = atorFilmeService;
        }
        [HttpGet("BuscaFilmesPorAtor")]
        public async Task<IActionResult> BuscaFilmesPorAtor([FromQuery] int idAtor)
        {
            if (idAtor <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var filmes = await _atorFilmeService.BuscarFilmesPorAtor(idAtor);

            if (filmes != null)
            {
                return Ok(filmes);
            }
            return BadRequest(new { message = "Filmes nao encontrados" });
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaAtorEmFilme")]
        public async Task<IActionResult> AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Result resultado = await _atorFilmeService.AdicionarAtorFilme(criarAtorFilmeDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlterarAtorDofilme")]
        public async Task<IActionResult> AlterarAtorDofilme([FromQuery] int idAtorAtual, int idFilme, int idAtorNovo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Result resultado = await _atorFilmeService.AlterarAtorDoFilme(idAtorAtual, idFilme, idAtorNovo);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtorDoFilme")]
        public async Task<IActionResult> DeletaAtorDoFilme([FromQuery] int idAtor, int idFilme)
        {
            Result resultado = await _atorFilmeService.DeletarAtorDoFilme(idAtor, idFilme);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }


    }
}
