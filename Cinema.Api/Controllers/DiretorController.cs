using Domain.Dtos.DiretorDto;
using Microsoft.AspNetCore.Mvc;
using Serviços.Services.Entities;

namespace Cinema.Api.Controllers
{
    [ApiController]
    [Route("controller")]
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
    }
}
