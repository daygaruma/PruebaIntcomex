using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Models;
using PruebaIntcomexApi.Utilidades;

namespace PruebaIntcomexApi.Manejadores
{
    public class ManejadorUsuario
    {
        private readonly ApplicationDbContext _db;
        public ManejadorUsuario(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Usuario> findByUser(string user)
        {
            Usuario result = await _db.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario.ToUpper() == user.ToUpper() && x.Estado == 1) ?? null;            
            return result;
        }

        public async Task<Usuario> findByID(int id)
        {
            Usuario result = await _db.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id && x.Estado == 1) ?? null;
            return result;
        }

        public async Task<List<Usuario>> findAll()
        {
            var lista = await _db.Usuarios.Where(c => c.Estado == 1).OrderBy(c => c.NombreUsuario).ToListAsync();
            return lista;
        }

        public async Task<bool> insert(UsuarioRequest _usuario)
        {

            Usuario userNew = new Usuario
            {
                NombreUsuario = _usuario.NombreUsuario.ToUpper()
            };
            _db.Usuarios.Add(userNew);
            await _db.SaveChangesAsync();

            return !string.IsNullOrEmpty(userNew.IdUsuario.ToString());
        }

        public async Task<bool> update(UsuarioRequest user, int id)
        {
            bool result = false;
            Usuario obj = await findByID(id);
            if (obj != null)
            {
                obj.NombreUsuario = user.NombreUsuario.ToUpper();
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (_db.Usuarios.Any(x => x.IdUsuario == obj.IdUsuario && x.NombreUsuario.Equals(obj.NombreUsuario)))
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> delete(int id)
        {
            bool result = false;
            Usuario obj = await findByID(id);
            if (obj != null)
            {
                obj.Estado = 0;
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (_db.Usuarios.Any(x => x.IdUsuario == obj.IdUsuario && x.Estado == 0))
                {
                    result = true;
                }
            }

            return result;
        }

    }
}
