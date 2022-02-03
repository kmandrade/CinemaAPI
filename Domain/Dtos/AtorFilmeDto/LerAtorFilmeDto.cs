using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AtorFilme
{
    public class LerAtorFilmeDto
    {
        public int Id { get; set; }
        public Ator Ator { get; set; }
        public Filme Filme { get; set; }
    }
}
