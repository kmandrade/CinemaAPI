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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //DIRETOR
            builder.Entity<Filme>()
                .HasOne(filme => filme.Diretor)
                .WithMany(diretor => diretor.Filmes)
                .HasForeignKey(filme => filme.DiretorId);

            //VOTOS
            builder.Entity<Votos>()
                .HasOne(v => v.Filme)
                .WithMany(f => f.Votos)
                .HasForeignKey(v => v.IdFilme);
            builder.Entity<Votos>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Votos)
                .HasForeignKey(v => v.IdUsuario);
            builder.Entity<Votos>()
                .HasKey(v => v.IdVotos);

            builder.Entity<Usuario>()
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
            builder.Entity<AtoresFilme>()
                .HasOne(atf => atf.Filme)
                .WithMany(filme=>filme.AtoresFilme)
                .HasForeignKey(atf=>atf.IdFilme);
            builder.Entity<AtoresFilme>()
                .HasOne(atf => atf.Ator)
                .WithMany(at => at.AtoresFilme)
                .HasForeignKey(atf => atf.IdAtor);
            builder.Entity<AtoresFilme>()
                .HasKey(atf=>atf.IdAtoresFilme);

            //GENEROFILME
            builder.Entity<GeneroFilme>()
                .HasOne(gf => gf.Filme)
                .WithMany(filme => filme.GenerosFilme)
                .HasForeignKey(gf => gf.IdFilme);
           builder.Entity<GeneroFilme>()
                .HasOne(gf=>gf.Genero)
                .WithMany(gen=>gen.GeneroFilmes)
                .HasForeignKey(gf=>gf.IdGenero);
            builder.Entity<GeneroFilme>()
                .HasKey(gf=>gf.IdGeneroFilme);



        }



    }
}
