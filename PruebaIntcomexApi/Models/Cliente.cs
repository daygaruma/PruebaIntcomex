using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIntcomexApi.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoCliente { get; set; }
        public string NombreCompleto { get; set; }
        public int Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdUsuario { get; set; }
        public int IdCargo { get; set; }
        public int IdTipoContacto { get; set; }
        public int Estado { get; set; } = 1;

    }
}
