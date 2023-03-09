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
    public class ManejadorCargo
    {
        private readonly ApplicationDbContext _db;
        public ManejadorCargo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Cargo> findByCargo(string cargo)
        {
            Cargo result = await _db.Cargos.FirstOrDefaultAsync(x => x.NombreCargo.ToUpper() == cargo.ToUpper() && x.Estado == 1) ?? null;
            return result;
        }

        public async Task<Cargo> findByID(int id)
        {
            Cargo result = await _db.Cargos.FirstOrDefaultAsync(x => x.IdCargo == id && x.Estado == 1) ?? null;
            return result;
        }

        public async Task<List<Cargo>> findAll()
        {
            var lista = await _db.Cargos.Where(c => c.Estado == 1).OrderBy(c => c.NombreCargo).ToListAsync();
            return lista;
        }

        public async Task<bool> insert(CargoRequest _cargo)
        {

            Cargo cargoNew = new Cargo
            {
                NombreCargo = _cargo.NombreCargo.ToUpper()
            };
            _db.Cargos.Add(cargoNew);
            await _db.SaveChangesAsync();

            return !string.IsNullOrEmpty(cargoNew.IdCargo.ToString());
        }

        public async Task<bool> update(CargoRequest cargo, int id)
        {
            bool result = false;
            Cargo obj = await findByID(id);
            if (obj != null)
            {
                obj.NombreCargo = cargo.NombreCargo.ToUpper();
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (_db.Cargos.Any(x => x.IdCargo == obj.IdCargo && x.NombreCargo.Equals(obj.NombreCargo)))
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> delete(int id)
        {
            bool result = false;
            Cargo obj = await findByID(id);
            if (obj != null)
            {
                obj.Estado = 0;
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (_db.Cargos.Any(x => x.IdCargo == obj.IdCargo && x.Estado == 0))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
