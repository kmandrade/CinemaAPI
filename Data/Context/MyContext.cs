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

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = protech-ssc6;Initial Catalog=CinemaMDb;Trusted_Connection=true;");

        }

        public DbSet<Ator> Atores { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }



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
                .WithOne(votos => votos.Filme);

            //ATOR
            builder.Entity<Filme>()
                //filme tem mutios atores e muitos atores tem muitos filmes
                .HasMany(filme => filme.Atores)
                .WithMany(autores => autores.Filmes)
                //foi utilizado o entity para configurar as relações dos argumentos de n:n
                .UsingEntity<Dictionary<string, object>>(
                    "FilmeAtor",
                    x => x
                    .HasOne<Ator>()
                    .WithMany()
                    .HasForeignKey("IdAtor")//tem chave estrangeira IdAtor
                    .HasConstraintName("FK_FilmeAtor_Atores_IdAtor")
                    .OnDelete(DeleteBehavior.Cascade),

                    x => x
                    .HasOne<Filme>()
                    .WithMany()
                    .HasForeignKey("IdFilme")//tem chave estrangeira IdFilme
                    .HasConstraintName("FK_FilmeAtor_Filmes_IdFilme")
                    .OnDelete(DeleteBehavior.ClientCascade));
                    
                
            //GENERO
            builder.Entity<Filme>()
                .HasMany(filme => filme.Generos)
                .WithMany(generos => generos.Filmes)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmeGenero",
                    x => x
                    .HasOne<Genero>()
                    .WithMany()
                    .HasForeignKey("IdGenero")
                    .HasConstraintName("FK_FilmeGenero_Generos_IdGenero")
                    .OnDelete(DeleteBehavior.Cascade),

                    x => x
                    .HasOne<Filme>()
                    .WithMany()
                    .HasForeignKey("IdFilme")
                    .HasConstraintName("FK_FilmeGenero_Filmes_IdFilme")
                    .OnDelete(DeleteBehavior.ClientCascade));



        }



    }
}
