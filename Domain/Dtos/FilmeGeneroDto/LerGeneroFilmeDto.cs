using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.FilmeGenero
{
    public class LerGeneroFilmeDto
    {
        public int Id { get; set; }
        public Filme Filme { get; set; }
        public Genero Genero { get; set; }
    }
}
