using Data.Entities;
using Domain.Models;
using Domain.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Handlers
{
    public class DefaultAdminService : IAdminService
    {
        
        IFilmeDao _filmeDao;
        IQueryBusca _buscaDao;

        public DefaultAdminService(IFilmeDao filmeDao, IQueryBusca queryBusca)
        {
            _filmeDao = filmeDao;
            _buscaDao = queryBusca;
        }

     
        public IEnumerable<Filme> ConsultaFilmes()
        {
            return _filmeDao.BuscarTodos();
        }
        public Filme ConsultaFilmePorId(int id)
        {
            return _filmeDao.BuscarPorId(id);
        }

        //public IEnumerable<Filme> ConsultaFilmesPorAutor(string nomeAutor)
        //{
        //    yield return _buscaDao.BuscaFilmePorAtor(nomeAutor);
        //}

        //public IEnumerable<Filme> ConsultaFilmesPorDiretor(string nomeDiretor)
        //{
        //    yield return _buscaDao.BuscaFilmePorDiretor(nomeDiretor);
        //}

        //public IEnumerable<Filme> ConsultaFilmesPorGenero(string nomeGenero)
        //{
        //    yield return _buscaDao.BuscaFilmePorGenero(nomeGenero);
        //}
        public void CadastraFilme(Filme filme)
        {
            _filmeDao.Incluir(filme);
        }

        public void ModificaFilme(Filme filme)
        {
            _filmeDao.Alterar(filme);
        }

        public void RemoveFilme(Filme filme)
        {
            _filmeDao.Excluir(filme);
        }
    }
}
