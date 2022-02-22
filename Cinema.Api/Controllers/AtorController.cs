using Domain.Dtos.AtorDto;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;

namespace Cinema.Api.Controllers
{
    [Authorize]
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
        public IActionResult ConsultaAtores([FromQuery] int skip, int take)
        {
            if (skip < 0 || take < 0) return BadRequest();
            var atores = _atorService.ConsultaTodos(skip,  take);
            if (atores == null) return BadRequest();
            return Ok(atores);
        }

        [HttpGet("ConsultaAtorPorId/{id}")]
        public IActionResult ConsultaAtorPorId(int id)
        {
            var atores = _atorService.ConsultaPorId(id);
            if (id < 0 || id == null || atores==null)
            {
                return BadRequest();
            }
            return Ok(atores);

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastraAtor([FromBody]CriarAtorDto atorDto)
        {
           Result resultado =  _atorService.Cadastra(atorDto);
            if (resultado.IsFailed)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraNomeAtor/{id}")]
        public IActionResult AlteraNomeAtor(int id, AlterarAtorDto obj)
        {
            _atorService.Altera(id, obj);
            return Ok();
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtor/{id}")]
        public IActionResult DeletaAtor(int id)
        {
            _atorService.Excluir(id);
            return Ok();
        }
    }
}
