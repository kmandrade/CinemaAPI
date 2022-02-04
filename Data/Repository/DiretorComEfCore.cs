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
    public class DiretorComEfCore : BaseRepository<Diretor>,IDiretorDao
    {
        private readonly DbSet<Diretor> _dbset;

        public DiretorComEfCore(MyContext _context):base(_context)
        {
            _dbset = _context.Set<Diretor>();

        }
        

       
    }
}
