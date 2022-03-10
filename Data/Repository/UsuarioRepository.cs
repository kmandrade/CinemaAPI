using Data.Context;
using Data.InterfacesData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly DbSet<Usuario> _dbSetUsuario;

        public UsuarioRepository(MyContext _context) : base(_context)
        {
            _dbSetUsuario = _context.Set<Usuario>();
        }

        public async Task<Usuario> BuscarUsuarioPorNomeESenha(string nome, string password)
        {
            var usuario = _context.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario == nome && u.Password == password);
            return await usuario;
        }
    }
}
