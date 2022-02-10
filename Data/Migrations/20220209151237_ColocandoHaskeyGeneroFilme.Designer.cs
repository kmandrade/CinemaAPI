﻿// <auto-generated />
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220209151237_ColocandoHaskeyGeneroFilme")]
    partial class ColocandoHaskeyGeneroFilme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Ator", b =>
                {
                    b.Property<int>("IdAtor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAtor"), 1L, 1);

                    b.Property<string>("NomeAtor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAtor");

                    b.ToTable("Atores");
                });

            modelBuilder.Entity("Domain.Models.AtoresFilme", b =>
                {
                    b.Property<int>("IdAtoresFilme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAtoresFilme"), 1L, 1);

                    b.Property<int>("IdAtor")
                        .HasColumnType("int");

                    b.Property<int>("IdFilme")
                        .HasColumnType("int");

                    b.HasKey("IdAtoresFilme");

                    b.HasIndex("IdAtor");

                    b.HasIndex("IdFilme");

                    b.ToTable("AtoresFilmes");
                });

            modelBuilder.Entity("Domain.Models.Diretor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NomeDiretor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diretores");
                });

            modelBuilder.Entity("Domain.Models.Filme", b =>
                {
                    b.Property<int>("IdFilme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFilme"), 1L, 1);

                    b.Property<int>("DiretorId")
                        .HasColumnType("int");

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFilme");

                    b.HasIndex("DiretorId");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("Domain.Models.Genero", b =>
                {
                    b.Property<int>("IdGenero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGenero"), 1L, 1);

                    b.Property<string>("NomeGenero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGenero");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("Domain.Models.GeneroFilme", b =>
                {
                    b.Property<int>("IdGeneroFilme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGeneroFilme"), 1L, 1);

                    b.Property<int>("IdFilme")
                        .HasColumnType("int");

                    b.Property<int>("IdGenero")
                        .HasColumnType("int");

                    b.HasKey("IdGeneroFilme");

                    b.HasIndex("IdFilme");

                    b.HasIndex("IdGenero");

                    b.ToTable("GenerosFilmes");
                });

            modelBuilder.Entity("Domain.Models.Votos", b =>
                {
                    b.Property<int>("IdVotos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVotos"), 1L, 1);

                    b.Property<int>("IdFilme")
                        .HasColumnType("int");

                    b.Property<int>("ValorDoVoto")
                        .HasColumnType("int");

                    b.HasKey("IdVotos");

                    b.HasIndex("IdFilme");

                    b.ToTable("Votos");
                });

            modelBuilder.Entity("Domain.Models.AtoresFilme", b =>
                {
                    b.HasOne("Domain.Models.Ator", "Ator")
                        .WithMany("AtoresFilme")
                        .HasForeignKey("IdAtor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Filme", "Filme")
                        .WithMany("AtoresFilme")
                        .HasForeignKey("IdFilme")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ator");

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("Domain.Models.Filme", b =>
                {
                    b.HasOne("Domain.Models.Diretor", "Diretor")
                        .WithMany("Filmes")
                        .HasForeignKey("DiretorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diretor");
                });

            modelBuilder.Entity("Domain.Models.GeneroFilme", b =>
                {
                    b.HasOne("Domain.Models.Filme", "Filme")
                        .WithMany("GenerosFilme")
                        .HasForeignKey("IdFilme")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Genero", "Genero")
                        .WithMany("GeneroFilmes")
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filme");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("Domain.Models.Votos", b =>
                {
                    b.HasOne("Domain.Models.Filme", "Filme")
                        .WithMany("Votos")
                        .HasForeignKey("IdFilme")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("Domain.Models.Ator", b =>
                {
                    b.Navigation("AtoresFilme");
                });

            modelBuilder.Entity("Domain.Models.Diretor", b =>
                {
                    b.Navigation("Filmes");
                });

            modelBuilder.Entity("Domain.Models.Filme", b =>
                {
                    b.Navigation("AtoresFilme");

                    b.Navigation("GenerosFilme");

                    b.Navigation("Votos");
                });

            modelBuilder.Entity("Domain.Models.Genero", b =>
                {
                    b.Navigation("GeneroFilmes");
                });
#pragma warning restore 612, 618
        }
    }
}
