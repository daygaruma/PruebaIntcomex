using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Manejadores;
using PruebaIntcomexApi.Models;
using PruebaIntcomexApi.Utilidades;

namespace PruebaIntcomexApi.Manejadores
{
    public class ManejadorTipoContacto
    {
        private readonly ApplicationDbContext _db;

        public ManejadorTipoContacto(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TipoContacto> findByTipo(string tipo)
        {
            TipoContacto result = await _db.TiposContactos.FirstOrDefaultAsync(x => x.NombreTipoContacto == tipo && x.Estado == 1) ?? null;
            return result;
        }

        public async Task<TipoContacto> findByID(int id)
        {
            TipoContacto result = await _db.TiposContactos.FirstOrDefaultAsync(x => x.IdTipoContacto == id && x.Estado == 1) ?? null;
            return result;
        }

        public async Task<List<TipoContacto>> findAll()
        {
            var lista = await _db.TiposContactos.Where(c => c.Estado == 1).OrderBy(c => c.NombreTipoContacto).ToListAsync();
            return lista;
        }

        public async Task<bool> insert(TipoContactRequest _tipo)
        {

            TipoContacto tipoNew = new TipoContacto
            {
                NombreTipoContacto = _tipo.NombreTipoContacto.ToUpper()
            };
            _db.TiposContactos.Add(tipoNew);
            await _db.SaveChangesAsync();

            return !string.IsNullOrEmpty(tipoNew.IdTipoContacto.ToString());
        }

        public async Task<bool> update(TipoContactRequest tipo, int id)
        {
            bool result = false; 
            TipoContacto obj = await findByID(id);
            if (obj != null) 
            {
                obj.NombreTipoContacto = tipo.NombreTipoContacto.ToUpper();
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                
                if (_db.TiposContactos.Any(x => x.IdTipoContacto == obj.IdTipoContacto && x.NombreTipoContacto.Equals(obj.NombreTipoContacto))) {
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> delete(int id)
        {
            bool result = false;
            TipoContacto obj = await findByID(id);
            if (obj != null)
            {
                obj.Estado = 0;
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (_db.TiposContactos.Any(x => x.IdTipoContacto == obj.IdTipoContacto && x.Estado == 0))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
