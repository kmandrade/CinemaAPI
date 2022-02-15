using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class UsuarioDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) : base(opts)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = protech-ssc6;Initial Catalog=CinemaMDb;Trusted_Connection=true;");

        }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>()
              .HasData(new Usuario
              {
                  IdUsuario = 1,
                  CargoUsuario = CargoUsuario.Administrador,
                  Password = "admin",
                  Email = "",
                  NomeUsuario = "admin",
                  Votos = null
              });

        }
    }
}
