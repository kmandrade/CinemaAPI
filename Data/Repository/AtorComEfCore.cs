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
    public class AtorComEfCore : BaseRepository<Ator>, IAtorDao
    {

        private readonly DbSet<Ator> _dbset;

        public AtorComEfCore(MyContext _context):base(_context)
        {
            _dbset = _context.Set<Ator>();
        }

        //public Ator BuscarPorNome(string nome)
        //{
        //    var _ator = _context.Atores.Where(a => a.NomeAtor == nome);
        //    return _ator.FirstOrDefault();
        //}

    }
}
