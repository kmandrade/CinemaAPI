using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AtorDto
{
    public class LerAtorDto
    {
        [Key]
        [Required]
        public int IdAtor { get; set; }
        [Required]
        public string NomeAtor { get; set; }


    }
}
