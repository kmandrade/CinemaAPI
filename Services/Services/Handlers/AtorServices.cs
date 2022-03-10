using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.AtorDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.InterfacesService;

namespace Servicos.Services.Handlers
{
    public class AtorServices : IAtorService
    {

        private readonly IAtorRepository _atorRepository;

        private readonly IMapper _mapper;
        public AtorServices(IMapper mapper, IAtorRepository atorRepository)
        {
            _atorRepository = atorRepository;
            _mapper = mapper;

        }


        public async Task<Result<LerAtorDto>> BuscarPorId(int id)
        {
            var atores = await _atorRepository.BuscarPorId(id);
            if (atores == null)
            {
                return Result.Fail("Ator nao encontrado");
            }
            var atorDto = _mapper.Map<LerAtorDto>(atores);
            return Result.Ok(atorDto);

        }

        public async Task<IEnumerable<LerAtorDto>> BuscarTodos(int skip, int take)
        {

            var atores = await _atorRepository.BuscarTodos();
            if (atores == null)
            {
                return null;
            }
            var atoresPaginados = atores.Skip(skip).Take(take).ToList();


            var atoresDto = _mapper.Map<IEnumerable<LerAtorDto>>(atoresPaginados);
            return atoresDto;
        }

        public async Task<Result> Cadastrarr(CriarAtorDto obj)
        {
            var buscaAtorExistente = await _atorRepository.BuscarPorNome(obj.NomeAtor);
            if (buscaAtorExistente != null)
            {
                return Result.Fail("ator ja existe");
            }
            var atorMapeado = _mapper.Map<Ator>(obj);
            await _atorRepository.Cadastrar(atorMapeado);
            return Result.Ok();
        }

        public async Task<Result> Alterar(int id, AlterarAtorDto obj)
        {
            var atorSelecionado = await _atorRepository.BuscarPorId(id);
            if (atorSelecionado == null)
            {
                return Result.Fail("Ator nao existe");
            }
            _mapper.Map(obj, atorSelecionado);
            await _atorRepository.Save();
            return Result.Ok();

        }

        public async Task<Result> Excluir(int id)
        {
            var atorSelecionado = await _atorRepository.BuscarPorId(id);
            if (atorSelecionado != null)
            {
                _atorRepository.Excluir(atorSelecionado);
                return Result.Ok();
            }
            return Result.Fail("Esse Ator nao existe");
        }


    }
}
