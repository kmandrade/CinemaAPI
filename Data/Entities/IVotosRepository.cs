using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public interface IVotosRepository: IRepository<Votos>
    {
        Task<IQueryable<Votos>> BuscaFilmesMaisVotados();
        Task<Votos> BuscaVotoPorFilmeEUsuario(int idFilme, int idUsuario);

    }
}
