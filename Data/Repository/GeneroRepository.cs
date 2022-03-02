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
    public class GeneroRepository : BaseRepository<Genero>,IGeneroRepository
    {
       private readonly DbSet<Genero> _dbset;

        public GeneroRepository(MyContext _context):base(_context)
        {
            _dbset = _context.Set<Genero>();
        }


   
    }
}
