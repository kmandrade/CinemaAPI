using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Cripitografia
{
    public interface IHash
    {

        void TransformaEmHash(Usuario usuario);
        Task<bool> ValidaSenha(Usuario usuario, string password1);

    }
}
