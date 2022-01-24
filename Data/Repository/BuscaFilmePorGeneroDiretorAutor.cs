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
    public class BuscaFilmePorGeneroDiretorAutor : IAtorDao,IDiretorDao,IGeneroDao
    {
        private readonly MyContext _context;

        public BuscaFilmePorGeneroDiretorAutor(MyContext context)
        {
            _context = context;
        }
        //include carregamento de entidades relacioandas 
        public Ator BuscarPorNomeDiretorOuGeneroOuAutor(object NomeDiretorOuAutorOuGenero)
        {
            return _context.Autores
                .Include(f => f.Filmes)
                .First(f => f.NomeAutor == NomeDiretorOuAutorOuGenero.ToString());
        }

        Genero IQuery<Genero>.BuscarPorNomeDiretorOuGeneroOuAutor(object NomeDiretorOuAutorOuGenero)
        {
            return _context.Generos
                .Include(f => f.Filmes)
                .First(f => f.TipoGenero == NomeDiretorOuAutorOuGenero.ToString());
        }

        Diretor IQuery<Diretor>.BuscarPorNomeDiretorOuGeneroOuAutor(object NomeDiretorOuAutorOuGenero)
        {
            return _context.Diretores
             .Include(f => f.Filmes)
             .First(f => f.NomeDiretor == NomeDiretorOuAutorOuGenero.ToString());
        }
    }
}
