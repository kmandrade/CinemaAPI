using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.AtorFilme;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.InterfacesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Handlers
{
    public class AtorFilmeServices : IAtorFilmeService
    {
        private readonly IAtorFilmeRepository _atorfilme;
        private readonly IAtorRepository _atorRepository;
        private readonly IFilmeRepository _filmeRepository;
        private readonly IMapper _mapper;

        public AtorFilmeServices(IMapper mapper, IAtorFilmeRepository atorfilme, IAtorRepository atorRepository, IFilmeRepository filmeRepository)
        {
            _mapper = mapper;

            _atorfilme = atorfilme;
            _atorRepository = atorRepository;
            _filmeRepository = filmeRepository;
        }



        public async Task<IEnumerable<LerAtorFilmeDto>> BuscarFilmesPorAtor(int idAtorFilme)
        {
            var atoresFilme = await _atorfilme.BuscarFilmesPorAtor(idAtorFilme);
            if (atoresFilme != null)
            {
                var atoresFilmeDto = _mapper.Map<IEnumerable<LerAtorFilmeDto>>(atoresFilme);
                return atoresFilmeDto;
            }
            //esse ator nao possui nenhum filme
            return null;
        }
        public async Task<Result> AdicionarAtorFilme(CriarAtorFilmeDto criarAtorFilmeDto)
        {
            var buscaAtor = await _atorRepository.BuscarPorId(criarAtorFilmeDto.IdAtor);
            var buscaFilme = await _filmeRepository.BuscarPorId(criarAtorFilmeDto.IdFilme);
            if (buscaAtor == null || buscaFilme==null)
            {
                return Result.Fail("Ator nao existe");
            }

            var atorFilme = _mapper.Map<AtoresFilme>(criarAtorFilmeDto);


            await _atorfilme.Cadastrar(atorFilme);
            return Result.Ok();



        }
        public async Task<Result> AlterarAtorDoFilme(int idAtorAtual, int idFilme, int idAtorNovo)
        {
            var verificaSeAtorNovoExiste = await _atorRepository.BuscarPorId(idAtorNovo);
            if (verificaSeAtorNovoExiste == null )
            {
                return Result.Fail("Novo Ator Nao Cadastrardo");
            }
            //verifica se o atorAtual ou Filme existem
            var AtorFilmeSelecionado = await _atorfilme.BuscarAtorEFilme(idAtorAtual, idFilme);
            if (AtorFilmeSelecionado != null)
            {
                AtorFilmeSelecionado.IdAtor = idAtorNovo;
                await _atorfilme.Save();
                return Result.Ok();
            }
            return Result.Fail("Dados nao Conferem");
        }

        public async Task<Result> DeletarAtorDoFilme(int idAtor, int idFilme)
        {
            var selecionarAtorDoFilme = await _atorfilme.BuscarAtorEFilme(idAtor, idFilme);
            if (selecionarAtorDoFilme != null)
            {
                _atorfilme.Excluir(selecionarAtorDoFilme);
                return Result.Ok();
            }
            return Result.Fail(errorMessage: "Dados nao Conferem");

        }


    }
}
