using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Models;
using PruebaIntcomexApi.Utilidades;

namespace PruebaIntcomexApi.Manejadores
{
    public class ManejadorCliente
    {
        private readonly ApplicationDbContext _db;
        public ManejadorCliente(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> insert(ClienteRequest _cliente)
        {            

            Cliente clienteNew = new Cliente {
                CorreoElectronico = _cliente.CorreoElectronico,
                IdCargo = _cliente.IdCargo,
                IdTipoContacto = _cliente.IdTipoContacto,
                IdUsuario = _cliente.IdUsuario,
                NombreCompleto = _cliente.NombreCompleto,
                Telefono = _cliente.Telefono
            };
            _db.Clientes.Add(clienteNew);
            await _db.SaveChangesAsync();

            return !string.IsNullOrEmpty(clienteNew.CodigoCliente.ToString());
        }

        public async Task<Cliente> findById(int id)
        {
            Cliente result = await _db.Clientes.FirstOrDefaultAsync(x => x.CodigoCliente == id && x.Estado == 1) ?? null;
            return result;
        }

        public async Task<List<Cliente>> findAll()
        {
            var lista = await _db.Clientes.Where(c => c.Estado == 1).OrderBy(c => c.NombreCompleto).ToListAsync();
            return lista;
        }

        public async Task<bool> update(ClienteRequest _cliente, int id)
        {
            bool result = false;
            Cliente obj = await findById(id);
            if (obj != null)
            {
                obj.NombreCompleto = _cliente.NombreCompleto.ToUpper();
                obj.CorreoElectronico = _cliente.CorreoElectronico;
                obj.IdCargo = _cliente.IdCargo;
                obj.IdTipoContacto = _cliente.IdTipoContacto;
                obj.IdUsuario = _cliente.IdUsuario;
                obj.Telefono = _cliente.Telefono;

                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                bool validateUpdate = _db.Clientes.Any(x => x.NombreCompleto.Equals(obj.NombreCompleto)
                                           && x.CorreoElectronico.Equals(obj.CorreoElectronico)
                                           && x.IdCargo.Equals(obj.IdCargo)
                                           && x.IdTipoContacto.Equals(obj.IdTipoContacto)
                                           && x.IdUsuario.Equals(obj.IdUsuario)
                                           && x.Telefono.Equals(obj.Telefono)                                       
                                       );
                if (validateUpdate)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> delete(int id)
        {
            bool result = false;
            Cliente obj = await findById(id);
            if (obj != null)
            {
                obj.Estado = 0;
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (_db.Clientes.Any(x => x.CodigoCliente == obj.CodigoCliente && x.Estado == 0))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
