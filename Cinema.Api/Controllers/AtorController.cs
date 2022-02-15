using Domain.Dtos.AtorDto;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtorController : ControllerBase
    {
        IAtorService _atorService;

        public AtorController(IAtorService atorService)
        {
            _atorService = atorService;
        }

        [HttpGet("ConsultaAtores")]
        public IActionResult ConsultaAtores()
        {
            var atores = _atorService.ConsultaTodos();
            return Ok(atores);
        }

        [HttpPost]
        public IActionResult CadastraAtor([FromBody]CriarAtorDto atorDto)
        {
            _atorService.Cadastra(atorDto);
            return Ok();
        }
    
        [HttpPut("AlteraNomeAtor/{id}")]
        public IActionResult AlteraNomeAtor(int id, AlterarAtorDto obj)
        {
            _atorService.Altera(id, obj);
            return Ok();
        }


        [HttpDelete("DeletaAtor/{id}")]
        public IActionResult DeletaAtor(int id)
        {
            _atorService.Excluir(id);
            return Ok();
        }
    }
}
