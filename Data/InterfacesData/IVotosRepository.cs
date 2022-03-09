using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.InterfacesData
{
    public interface IVotosRepository: IRepository<Votos>
    {
        Task<Votos> BuscarVotoPorFilme(int idFilme);
        Task<Votos> BuscarVotoPorFilmeEUsuario(int idFilme, int idUsuario);

    }
}
