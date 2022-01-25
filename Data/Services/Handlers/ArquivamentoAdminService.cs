using Data.Entities;
using Domain.Models;
using Domain.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Handlers
{
    public class ArquivamentoAdminService : IAdminService
    {

        IAdminService _defaultService;

        public ArquivamentoAdminService(IFilmeDao filmeDao, IQueryBusca queryBusca)
        {
          //  _defaultService = defaultService;
        }

        public void CadastraFilme(Filme filme)
        {
            throw new NotImplementedException();
        }

        public Filme ConsultaFilmePorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Filme> ConsultaFilmes()
        {
            throw new NotImplementedException();
        }

        public void ModificaFilme(Filme filme)
        {
            throw new NotImplementedException();
        }

        public void RemoveFilme(Filme filme)
        {
            throw new NotImplementedException();
        }
    }
}
