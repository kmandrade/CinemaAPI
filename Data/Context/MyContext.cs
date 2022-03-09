using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;  

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = protech-ssc6;Initial Catalog=CinemaMDB;Trusted_Connection=true;");
        }

        public DbSet<Ator> Atores { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<AtoresFilme> AtoresFilmes { get;set; }
        public DbSet<GeneroFilme> GenerosFilmes { get;set; }
        public DbSet<Votos> Votos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //DIRETOR
            modelBuilder.Entity<Filme>()
                .HasOne(filme => filme.Diretor)
                .WithMany(diretor => diretor.Filmes)
                .HasForeignKey(filme => filme.DiretorId);

            //VOTOS
            modelBuilder.Entity<Votos>()
                .HasOne(v => v.Filme)
                .WithMany(f => f.Votos)
                .HasForeignKey(v => v.IdFilme);
            modelBuilder.Entity<Votos>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Votos)
                .HasForeignKey(v => v.IdUsuario);
            modelBuilder.Entity<Votos>()
                .HasKey(v => v.IdVotos);

            modelBuilder.Entity<Usuario>()
                .HasData(new Usuario
                {
                    IdUsuario = 1,
                    CargoUsuario = CargoUsuario.Administrador,
                    Password = "admin",
                    Email = "",
                    NomeUsuario = "admin",
                    Votos = null,
                    Situacao=SituacaoEntities.Ativado
                });

            //ATORFILME
            modelBuilder.Entity<AtoresFilme>()
                .HasOne(atoresFilme => atoresFilme.Filme)
                .WithMany(filme=>filme.AtoresFilme)
                .HasForeignKey(atoresFilme=>atoresFilme.IdFilme);
            modelBuilder.Entity<AtoresFilme>()
                .HasOne(atoresFilme => atoresFilme.Ator)
                .WithMany(at => at.AtoresFilme)
                .HasForeignKey(atoresFilme => atoresFilme.IdAtor);
            modelBuilder.Entity<AtoresFilme>()
                .HasKey(atoresFilme=>atoresFilme.IdAtoresFilme);

            //GENEROFILME
            modelBuilder.Entity<GeneroFilme>()
                .HasOne(generoFilme => generoFilme.Filme)
                .WithMany(filme => filme.GenerosFilme)
                .HasForeignKey(generoFilme => generoFilme.IdFilme);
           modelBuilder.Entity<GeneroFilme>()
                .HasOne(generoFilme=>generoFilme.Genero)
                .WithMany(gen=>gen.GeneroFilmes)
                .HasForeignKey(generoFilme=>generoFilme.IdGenero);
            modelBuilder.Entity<GeneroFilme>()
                .HasKey(generoFilme=>generoFilme.IdGeneroFilme);



        }



    }
}
