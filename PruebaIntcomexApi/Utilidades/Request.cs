using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIntcomexApi.Utilidades
{
    public class ClienteRequest
    {
        public string NombreCompleto { get; set; }
        public int Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdUsuario { get; set; }
        public int IdCargo { get; set; }
        public int IdTipoContacto { get; set; }
    }

    public class UsuarioRequest 
    {
        public string NombreUsuario { get; set; }
    }

    public class CargoRequest
    {
        public string NombreCargo { get; set; }
    }

    public class TipoContactRequest
    {
        public string NombreTipoContacto { get; set; }
    }
}
