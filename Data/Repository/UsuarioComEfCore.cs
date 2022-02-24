using Data.Context;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsuarioComEfCore: BaseRepository<Usuario>,IUsuarioDao
    {
        private readonly DbSet<Usuario> _dbSetUsuario;

        public UsuarioComEfCore(MyContext _context):base(_context)
        {
            _dbSetUsuario=_context.Set<Usuario>();
        }

        public async Task<Usuario> BuscaUsuarioPorNomeESenha(string nome, string password)
        {
            var usuario=_context.Usuarios
                .FirstOrDefaultAsync(u=>u.NomeUsuario==nome && u.Password==password);
            return await usuario;
        }
    }
}
