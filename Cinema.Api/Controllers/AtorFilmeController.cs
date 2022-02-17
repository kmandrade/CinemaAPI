﻿using Domain.Dtos.AtorFilme;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.Services.Entities;
using Servicos.Services.Handlers;

namespace Cinema.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AtorFilmeController : ControllerBase

    {
        private IAtorFilmeService _atorFilmeService;

        public AtorFilmeController(IAtorFilmeService atorFilmeService)
        {
            _atorFilmeService = atorFilmeService;
        }
        [HttpGet("LerFilmesPorAtor")]
        public IActionResult LerFilmesPorAtor([FromQuery] int idAtor)
        {
            var filmes = _atorFilmeService.BuscaFilmesPorAtor(idAtor);
            return Ok(filmes);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("AlteraAtorDofilme")]
        public IActionResult AlteraAtorDofilme([FromQuery]int idAtorAtual,int idFilme,int idAtorNovo)
        {
            _atorFilmeService.AlteraAtorDoFilme(idAtorAtual, idFilme, idAtorNovo);
            return Ok();
        }



        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionaAtorEmFilme")]
        public IActionResult AdicionaAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            _atorFilmeService.AdicionaAtorFilme(criarAtorFilmeDto);
            return Ok();
        }


        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeletaAtorDoFilme")]
        public IActionResult DeletaAtorDoFilme([FromQuery] int idAtor,int idFilme)
        {
            _atorFilmeService.DeletaAtorDoFilme(idAtor,idFilme);
            return Ok();
        }
        

    }
}
