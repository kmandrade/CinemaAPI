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
    public class BuscaFilmePorGeneroDiretorAutor : IQueryBusca
    {
        private readonly MyContext _context;

        public BuscaFilmePorGeneroDiretorAutor(MyContext context)
        {
            _context = context;
        }
        //Include tipo de carregamento dos dados, include é um carregamento rapido, consulta uma entidade relacionada
        public Diretor BuscaFilmePorDiretor(string nomeDiretor)
        {
            return _context.Diretores
             .Include(f => f.Filmes)
             .First(f => f.NomeDiretor == nomeDiretor);
        }
        //yield serve para gerar um enmerable de qualquer coisa
        public IEnumerable<Ator> BuscaFilmePorAtor(string NomeAtor)
        {
            yield return _context.Atores
                           .Include(f => f.Filmes)
                          .First(f => f.NomeAtor == NomeAtor);
        }

        public IEnumerable<Genero> BuscaFilmePorGenero(string nomeGenero)
        {
            yield return _context.Generos
               .Include(f => f.Filmes)
                .First(f => f.TipoGenero == nomeGenero.ToString());
        }
        //include carregamento de entidades relacioandas 



      
    }
}
