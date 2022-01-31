using Domain.Dtos.DiretorDto;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Entities;

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

        [HttpPost]
        public IActionResult CadastraDiretor([FromBody]CriarDiretorDto criarDiretorDto)
        {
            _diretorService.Cadastra(criarDiretorDto);
            return Ok();
        }
        [HttpGet]
        public IActionResult BuscaFilmesPorDiretor([FromQuery] LerDiretorDto lerDiretorDto)
        {
           var filmes = _diretorService.lerFilmeDtosPorDiretor(lerDiretorDto);
            if (filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult BuscaDiretores()
        {
            var diretores= _diretorService.ConsultaTodos();

            if(diretores != null)
            {
                return Ok(diretores);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult BuscaDiretoresPorId([FromQuery] int idDiretor)
        {
            var diretor = _diretorService.ConsultaPorId(idDiretor);

            if(diretor != null)
            {
                return Ok(diretor);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult RemoveDiretor([FromQuery] LerDiretorDto diretor)
        {
            _diretorService.Remove(diretor);
            return NoContent();
            
        }
        [HttpGet]
        public IActionResult ModificaDiretor([FromQuery] AlterarDiretorDto diretor)
        {
            _diretorService.Altera(diretor);
            return NoContent();

        }

    }
}
