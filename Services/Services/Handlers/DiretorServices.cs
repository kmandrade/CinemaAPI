using AutoMapper;
using Data.InterfacesData;
using Domain.Dtos.DiretorDto;
using Domain.Models;
using FluentResults;
using Servicos.Services.InterfacesService;

namespace Servicos.Services.Handlers
{
    public class DiretorServices : IDiretorService
    {
        private readonly IDiretorRepository _diretorRepository;

        private readonly IMapper _mapper;
        public DiretorServices(IMapper mapper, IDiretorRepository diretorRepository)
        {
            _mapper = mapper;
            _diretorRepository = diretorRepository;


        }



        public async Task<IEnumerable<LerDiretorDto>> BuscarTodos(int skip, int take)
        {
            var diretores = await _diretorRepository.BuscarTodos();
            if (diretores == null)
            {
                return null;
            }
            var diretoresPaginados = diretores.Skip(skip).Take(take).ToList();
            var diretoresMapeados = _mapper.Map<IEnumerable<LerDiretorDto>>(diretoresPaginados);
            return diretoresMapeados;
        }

        public async Task<Result<LerDiretorDto>> BuscarPorId(int id)
        {
            var diretor = await _diretorRepository.BuscarPorId(id);
            if (diretor == null)
            {
                return Result.Fail("Diretor nao encontrado");
            }

            var diretorDto = _mapper.Map<LerDiretorDto>(diretor);//converte pra dto e manda pra tela
            return Result.Ok(diretorDto);
        }

        public async Task<Result> Cadastrar(CriarDiretorDto obj)
        {
            var buscarDiretor = await _diretorRepository.BuscarDiretorPorNome(obj.NomeDiretor);
            if (buscarDiretor != null)
            {
                return Result.Fail("Diretor ja existe");
            }
            var diretor = _mapper.Map<Diretor>(obj);
            await _diretorRepository.Cadastrar(diretor);
            return Result.Ok();
        }

        public async Task<Result> Alterar(int id, AlterarDiretorDto diretorDto)
        {
            var diretorSelecionado = await _diretorRepository.BuscarPorId(id);
            if (diretorSelecionado == null) { return Result.Fail("Diretor nao existe"); }

            var diretorMapeado = _mapper.Map<Diretor>(diretorDto);
            await _diretorRepository.Alterar(diretorMapeado);
            return Result.Ok();



        }

        public async Task<Result> Excluir(int id)
        {
            var diretorSelecionado = await _diretorRepository.BuscarPorId(id);
            if (diretorSelecionado == null)
            {
                return Result.Fail("Diretor nao existe");
            }

            _diretorRepository.Excluir(diretorSelecionado);
            return Result.Ok();


        }


    }
}
