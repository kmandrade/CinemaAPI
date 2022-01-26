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
    public class FilmeComEfCore : IFilmeDao
    {

        private readonly MyContext _context;

        public FilmeComEfCore(MyContext context)
        {
            _context = context;
        }
        //FIND: encontra uma entidade com os valores de chave primária fornecidos.
        //Se uma entidade com os valores de chave primária fornecidos for rastreada pelo contexto,
        //ela será retornada imediatamente sem fazer uma solicitação ao banco de dados
        public Filme BuscarPorId(int id)
        {
            return _context.Filmes.Find(id);   
        }
        //retornar em ordem alfabetica
        public IEnumerable<Filme> BuscarTodos()
        {
            
            IEnumerable<Filme> query = _context.Filmes;//Era .ToList depois coloquei em um ienumerable
            //orderby ja é um enumerable
            return  query.OrderBy(nome => nome.Titulo);
    
        }

         public IEnumerable<Filme> BuscaFilmesPorAtor(object ator)
        {
                yield return _context.Filmes
                 .Include(a => a.Atores)
                 .First(f => f.Atores == ator);
        }

        public IEnumerable<Filme> BuscaFilmesPorDiretor(object _diretor)
        {
            yield return _context.Filmes
               .Include(a => a.Diretor)
               .First(f => f.Diretor == _diretor);
        }

        public IEnumerable<Filme> BuscaFilmesPorGenero(object genero)
        {
            yield return _context.Filmes
                .Include(g => g.Generos)
                .First(f => f.Generos == genero);
        }



        public void Incluir(Filme obj)
        {
            _context.Filmes.Add(obj);
            _context.SaveChanges();
        }
          public void Alterar(Filme obj)
        {
            _context.Filmes.Update(obj);
            _context.SaveChanges();
        }

        public void Excluir(Filme obj)
        {
            _context.Filmes.Remove(obj);
            _context.SaveChanges();
        }

       
    }
}
