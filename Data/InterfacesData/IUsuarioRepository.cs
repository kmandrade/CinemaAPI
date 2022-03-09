using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.InterfacesData
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> BuscarUsuarioPorNomeESenha(string nome,string password);
    }
}
