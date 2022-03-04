using Domain.Dtos.FilmeGenero;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeneroFilmeController : ControllerBase
    {
        private IGeneroFilmeService _generoFilmeService;

        public GeneroFilmeController(IGeneroFilmeService generoFilmeService)
        {
            _generoFilmeService = generoFilmeService;
        }


        [HttpGet("BuscaFilmesPorGenero")]
        public async Task<IActionResult> BuscarFilmesPorGenero([FromQuery] int iDGenero)
        {
            var gf = await _generoFilmeService.BuscaFilmesPorGenero(iDGenero);
            return Ok(gf);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaGeneroEmFilme")]
        public async Task<IActionResult> AdicionaGeneroFilme(CriarGeneroFilmeDto criarGeneroFilmeDto)
        {
            await _generoFilmeService.AdicionaGeneroFilme(criarGeneroFilmeDto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraGeneroDoFilme")]
        public async Task<IActionResult> AlteraGeneroDoFilme(int idGeneroAntigo, int idFilme, int iDGeneroNovo)
        {
            await _generoFilmeService.AlteraGeneroDoFilme(idGeneroAntigo, idFilme, iDGeneroNovo);
            return Ok ();
        }

        

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaGeneroDoFilme")]
        public async Task<IActionResult> DeletaGeneroDoFilme([FromQuery] int idGenero,int idFilme)
        {
             await _generoFilmeService.DeletaGeneroDoFilme(idGenero,idFilme);
            return Ok();
        }
        
        
    }
}
