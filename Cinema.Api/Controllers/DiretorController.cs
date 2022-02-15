using Domain.Dtos.DiretorDto;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
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
        public IActionResult BuscaFilmesPorDiretor([FromQuery] int iDdiretor)
        {
            var filmes = _diretorService.lerFilmeDtosPorDiretor(iDdiretor);
            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }
        [HttpGet("BuscaTodosDiretores")]
        public IActionResult BuscaDiretores()
        {
            var diretores = _diretorService.ConsultaTodos();

            if (diretores != null)
            {
                return Ok(diretores);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult BuscaDiretoresPorId(int idDiretor)
        {
            var diretor = _diretorService.ConsultaPorId(idDiretor);

            if (diretor != null)
            {
                return Ok(diretor);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult CadastraDiretor([FromBody]CriarDiretorDto criarDiretorDto)
        {
            _diretorService.Cadastra(criarDiretorDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveDiretor(int id)
        {
            _diretorService.Excluir(id);
            return NoContent(); 
            
        }
        [HttpPut("{id}")]
        public IActionResult ModificaDiretor( int id, [FromBody] AlterarDiretorDto diretor)
        {
            _diretorService.Altera(id,diretor);
            return NoContent();

        }

    }
}
