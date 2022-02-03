using Domain.Dtos.AtorDto;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Entities;

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
        [HttpPost]
        public IActionResult CadastraAtor([FromBody]CriarAtorDto atorDto)
        {
            _atorService.Cadastra(atorDto);
            return Ok();
        }
        [HttpGet("Consulta Atores")]
        public IActionResult ConsultaAtores()
        {
           var atores =  _atorService.ConsultaTodos();
            return Ok(atores);
        }
       

        [HttpDelete("Deleta Ator{id}")]
        public IActionResult DeletaAtor(int id)
        {
            _atorService.Excluir(id);
            return Ok();
        }
    }
}
