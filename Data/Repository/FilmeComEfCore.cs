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
            
            IEnumerable<Filme> filmes = _context.Filmes;//Era .ToList depois coloquei em um ienumerable
            //orderby ja é um enumerable
            return filmes;
    
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
