using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Cripitografia
{
    public class Hash: IHash
    {

        public void TransformaEmHash(Usuario usuario)
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            usuario.Password = passwordHasher.HashPassword(usuario, usuario.Password);
        }

        public async Task<bool> ValidaSenha(Usuario usuario, string password1)
        {
            var passwordHash = new PasswordHasher<Usuario>();

            var status = passwordHash.VerifyHashedPassword(usuario, password1, usuario.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;
                    break;
                case PasswordVerificationResult.Success:
                    return true;
                    break;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    //chama o update para converter o hash de novo
                    return false;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

    }



}

