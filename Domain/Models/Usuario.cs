using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string NomeUsuario { get; set; }

        public string Email { get; set; }

        
        [DataType(DataType.Password)]//inferindo que sera do tipo password
        public string Password { get; set; }
        
        public List<Votos> Votos { get; set; }

        public CargoUsuario CargoUsuario { get; set; }
    }
}
