using AutoMapper;
using Data.Entities;
using Domain.Dtos.DiretorDto;
using Domain.Dtos.FilmeDto;
using Domain.Models;
using Serviços.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços.Services.Handlers
{
    public class DiretorServices : IDiretorService
    {
        IDiretorDao _diretorDao;
        IFilmeDao _filmeDao;
        private readonly IMapper _mapper;
        public DiretorServices(IDiretorDao diretorDao, IMapper mapper , IFilmeDao filmeDao)
        {
            _mapper=mapper;
            _diretorDao=diretorDao;
            _filmeDao=filmeDao;

        }

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(LerDiretorDto diretorDto)
        {
            var filmes = _filmeDao.BuscarTodos();
            var diretor = _mapper.Map<Diretor>(diretorDto);
            var queryFilmes = from filme in filmes where filme.Diretor== diretor select filme;
            var filmesDto=_mapper.Map<IEnumerable<LerFilmeDto>>(queryFilmes);
            return filmesDto;
        }

        public IEnumerable<LerDiretorDto> ConsultaTodos()
        {
            var diretores = _diretorDao.BuscarTodos();
            var diretoresDto = _mapper.Map<IEnumerable<LerDiretorDto>>(diretores);
            return diretoresDto;
        }

        public LerDiretorDto ConsultaPorId(int id)
        {
            var diretor = _diretorDao.BuscarPorId(id);//vai pegar so o numero do id e verifica no banco
            var diretorDto = _mapper.Map<LerDiretorDto>(diretor);//converte pra dto e manda pra tela
            return diretorDto;
        }

        public void Cadastra(CriarDiretorDto obj)
        {
            var diretor = _mapper.Map<Diretor>(obj);
            _diretorDao.Incluir(diretor);
        }

        public void Altera(int id, AlterarDiretorDto diretorDto)
        {
            var listaDiretores = _diretorDao.BuscarTodos(); //selecionei todos diretores 
            var diretorSelecionado = listaDiretores.FirstOrDefault(d=>d.Id == id); //peguei ele por id 
            var diretorMapeado = _mapper.Map<Diretor>(diretorDto);//transformei o dto em diretor para assim alterar
            _diretorDao.Alterar(diretorMapeado);
        }

        public void Excluir(int id)
        {
            var listaDiretores = _diretorDao.BuscarTodos(); //selecionei todos diretores 
            var diretorSelecionado = listaDiretores.FirstOrDefault(d => d.Id == id); //peguei ele por id 
            _diretorDao.Excluir(diretorSelecionado);
        }
    }
}
