using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PruebaIntcomexApi.Models
{
    public class Cargo
    {
        [Key]
        public int IdCargo { get; set; }
        public string NombreCargo { get; set; }
        public int Estado { get; set; } = 1;
    }
}
