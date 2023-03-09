using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PruebaIntcomexApi.Models
{
    public class TipoContacto
    {
        [Key]
        public int IdTipoContacto { get; set; }
        public string NombreTipoContacto { get; set; }
        public int Estado { get; set; } = 1;
    }
}
