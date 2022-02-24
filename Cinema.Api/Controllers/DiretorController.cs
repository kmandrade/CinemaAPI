using Domain.Dtos.DiretorDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DiretorController : ControllerBase
    {
        IDiretorService _diretorService;

        public DiretorController(IDiretorService diretorService)
        {
            _diretorService = diretorService;
        }
        [HttpGet("BuscaFilmesPorDiretor")]
        public async Task<IActionResult> BuscaFilmesPorDiretor([FromQuery] int iDdiretor)
        {
            var filmes = await _diretorService.lerFilmeDtosPorDiretor(iDdiretor);
            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }
        [HttpGet("BuscaTodosDiretores")]
        public async Task<IActionResult> BuscaDiretores([FromQuery]int skip, int take)
        {
            var diretores = await _diretorService.ConsultaTodos(skip, take);
            return Ok(diretores);
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaDiretoresPorId(int idDiretor)
        {
            var diretor = await _diretorService.ConsultaPorId(idDiretor);

            if (diretor != null)
            {
                return Ok(diretor);
            }
            return NotFound();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CadastraDiretor([FromBody]CriarDiretorDto criarDiretorDto)
        {
            await _diretorService.Cadastra(criarDiretorDto);
            return Ok();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificaDiretor(int id, [FromBody] AlterarDiretorDto diretor)
        {
            await _diretorService.Altera(id, diretor);
            return NoContent();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDiretor(int id)
        {
            await _diretorService.Excluir(id);
            return NoContent(); 
            
        }
       

    }
}
