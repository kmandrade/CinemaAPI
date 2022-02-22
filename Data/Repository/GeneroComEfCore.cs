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
    public class GeneroComEfCore : BaseRepository<Genero>,IGeneroDao
    {
       private readonly DbSet<Genero> _dbset;

        public GeneroComEfCore(MyContext _context):base(_context)
        {
            _dbset = _context.Set<Genero>();
        }


   
    }
}
