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
            optionsBuilder.UseSqlServer("Data Source = protech-ssc6;Initial Catalog=CinemaMDb;Trusted_Connection=true;");

        }

        public DbSet<Ator> Atores { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<AtoresFilme> AtoresFilmes { get;set; }
        public DbSet<GeneroFilme> GenerosFilmes { get;set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            //DIRETOR
            builder.Entity<Filme>()
                .HasOne(filme => filme.Diretor)
                .WithMany(diretor => diretor.Filmes)
                .HasForeignKey(filme => filme.DiretorId);
            //VOTOS
            builder.Entity<Filme>()
                .HasMany(filme => filme.Votos)
                .WithOne(votos => votos.Filme)
                .HasForeignKey(x=>x.IdFilme);

            //ATORFILME
            builder.Entity<AtoresFilme>()
                .HasOne(atf => atf.Filme)
                .WithMany(filme=>filme.AtoresFilme)
                .HasForeignKey(atf=>atf.IdFilme);
            builder.Entity<AtoresFilme>()
                .HasOne(atf => atf.Ator)
                .WithMany(at => at.AtoresFilme)
                .HasForeignKey(atf => atf.IdAtor);
            //GENEROFILME
            builder.Entity<GeneroFilme>()
                .HasOne(gf => gf.Filme)
                .WithMany(filme => filme.GenerosFilme)
                .HasForeignKey(gf => gf.IdFilme);
           builder.Entity<GeneroFilme>()
                .HasOne(gf=>gf.Genero)
                .WithMany(gen=>gen.GeneroFilmes)
                .HasForeignKey(gf=>gf.IdGenero);


            //builder.Entity<Filme>()
            //    .HasMany(f => f.Atores)
            //    .WithMany(a => a.Filmes)
            //    .UsingEntity<AtorFilme>(
            //    x => x
            //    .HasOne(at => at.Ator)
            //    .WithMany(a => a.AtorFilmes)
            //    .HasForeignKey(at => at.IdAtor),
            //    x => x
            //    .HasOne(at => at.Filme)
            //    .WithMany(f => f.AtorFilmes)
            //    .HasForeignKey(at => at.IdFilme),
            //    x =>
            //    {
            //        x.HasKey(f => new { f.IdFilme, f.IdAtor });
            //    });

            //GENERO
            //builder.Entity<Filme>()
            //    .HasMany(f => f.Generos)
            //    .WithMany(g => g.Filmes)
            //    .UsingEntity<GeneroFilme>(
            //    x => x
            //    .HasOne(gf => gf.Genero)
            //    .WithMany(g => g.GeneroFilmes)
            //    .HasForeignKey(gf => gf.IdGenero),
            //    x => x
            //    .HasOne(gf => gf.Filme)
            //    .WithMany(g => g.GeneroFilmes)
            //    .HasForeignKey(gf => gf.IdFilme),
            //    x =>
            //    {
            //        x.HasKey(g => new { g.IdFilme, g.IdGenero });
            //    });
        }



    }
}
