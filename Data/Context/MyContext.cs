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
                .WithOne(votos => votos.Filme)
                .HasForeignKey(x=>x.IdFilme);

            //ATOR
            builder.Entity<Filme>()
                //filme tem mutios atores e muitos atores tem muitos filmes
                .HasMany(filme => filme.Atores)
                .WithMany(autores => autores.Filmes);
                
            //GENERO
            builder.Entity<Filme>()
                .HasMany(filme => filme.Generos)
                .WithMany(generos => generos.Filmes);
                

        }



    }
}
