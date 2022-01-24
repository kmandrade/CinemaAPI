using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ator
    {

        [Key]
        public int AutorId { get; set; }

        public string NomeAutor { get; set; }


        public virtual IEnumerable<Filme> Filmes { get; set; }


    }
}
