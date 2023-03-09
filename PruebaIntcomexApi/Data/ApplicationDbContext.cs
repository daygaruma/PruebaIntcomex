using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIntcomexApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<TipoContacto> TiposContactos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
