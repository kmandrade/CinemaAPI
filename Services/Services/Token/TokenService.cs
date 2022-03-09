using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Servicos.Services.InterfacesService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.Token
{
    public  class TokenService: ITokenService
    {
        public  string GenerateToken(Usuario user) //retorna string , token final
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //vai gerar o token de fato
            var key = Encoding.ASCII.GetBytes(Settings.Secret);//encodar a chave em um array de bytes
            var tokenDescriptor = new SecurityTokenDescriptor//vai descrever tudo que o nosso token tem
            {
                Subject = new ClaimsIdentity(new Claim[] // roles do usuario
                {
                    new Claim(ClaimTypes.Name,user.NomeUsuario),
                    new Claim("Id",user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role,user.CargoUsuario.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(8), //precisa do refresh token
                SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature) //credenciais que ele vai utilizar para encripitar o token
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
