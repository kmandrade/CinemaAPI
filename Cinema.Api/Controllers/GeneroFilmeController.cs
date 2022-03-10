using Domain.Dtos.FilmeGenero;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.InterfacesService;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeneroFilmeController : ControllerBase
    {
        private readonly IGeneroFilmeService _generoFilmeService;

        public GeneroFilmeController(IGeneroFilmeService generoFilmeService)
        {
            _generoFilmeService = generoFilmeService;
        }


        [HttpGet("BuscaFilmesPorGenero/{id}")]
        public async Task<IActionResult> BuscarFilmesPorGenero([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var generoFilme = await _generoFilmeService.BuscarFilmesPorGenero(id);
            if (generoFilme == null)
            {
                return BadRequest(new { message = "Nao foi possivel encontrar o genero" });
            }
            return Ok(generoFilme);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaGeneroEmFilme")]
        public async Task<IActionResult> AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _generoFilmeService.AdicionarGeneroFilme(criarGeneroFilmeDto);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlterarGeneroDoFilme")]
        public async Task<IActionResult> AlterarGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo)
        {
            if (idGeneroAntigo <= 0 || idFilme <= 0 || iDGeneroNovo <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado = await _generoFilmeService.AlterarGeneroDoFilme(idGeneroAntigo, idFilme, iDGeneroNovo);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }



        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaGeneroDoFilme")]
        public async Task<IActionResult> DeletaGeneroDoFilme([FromQuery] int idGenero, int idFilme)
        {
            if (idGenero <= 0 || idFilme <= 0)
            {
                return BadRequest(new { message = "O id precisa ser maior que 0" });
            }
            var resultado = await _generoFilmeService.DeletarGeneroDoFilme(idGenero, idFilme);
            if (resultado.IsFailed)
            {
                return BadRequest(new { message = resultado.ToString() });
            }
            return Ok();
        }


    }
}
