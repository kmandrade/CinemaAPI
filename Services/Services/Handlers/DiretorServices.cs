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

        public IEnumerable<LerFilmeDto> lerFilmeDtosPorDiretor(int idDiretor)
        {

            var filmes = _diretorDao.BuscaFilmesPorDiretor(idDiretor);
            var filmesDto = _mapper.Map<IEnumerable<LerFilmeDto>>(filmes);
            return filmesDto;
            //var filmes = _filmeDao.BuscarPorId(idDiretor);

            //    var filmesDto = _mapper.Map<LerFilmeDto>(filmes);
            //    yield return filmesDto;
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
            var diretorSelecionado = _diretorDao.BuscarPorId(id);
            if(diretorSelecionado != null)
            {
                var diretorMapeado = _mapper.Map<Diretor>(diretorDto);
                _diretorDao.Alterar(diretorMapeado);
            }
            
        }

        public void Excluir(int id)
        {
            var diretorSelecionado = _diretorDao.BuscarPorId(id);
            if (diretorSelecionado != null)
            {
                _diretorDao.Excluir(diretorSelecionado);
            }
        }
    }
}
