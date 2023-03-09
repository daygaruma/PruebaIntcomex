using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Manejadores;
using PruebaIntcomexApi.Models;
using PruebaIntcomexApi.Utilidades;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace PruebaIntcomexApi.Controllers
{
    [EnableCors("permitir")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class TipoContactoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TipoContactoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> getTiposContactos()
        {
            try
            {
                var lista = await new ManejadorTipoContacto(_db).findAll();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tipo}")]
        public async Task<IActionResult> getTipoContacto(string tipo)
        {
            try
            {
                TipoContacto obj = await new ManejadorTipoContacto(_db).findByTipo(tipo);
                if (obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> guardarTipoContacto([FromBody] TipoContactRequest tipo)
        {

            try
            {
                if (tipo == null)
                {
                    throw new Exception("el tipo de contacto ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }

                TipoContacto obj = await new ManejadorTipoContacto(_db).findByTipo(tipo.NombreTipoContacto);
                
                if (obj != null)
                {
                    throw new Exception("el tipo de contacto ingresado ya existe");
                }

                bool resultadoInsert = await new ManejadorTipoContacto(_db).insert(tipo);

                if (resultadoInsert)
                {
                    return Ok("Tipo de contacto guardado correctamente!");
                }
                else
                {
                    throw new Exception("Tipo de contacto no guardado");
                }
            }
            catch (Exception ex)
            {
                string message = string.Empty;
                if (ModelState.Values.Any(v => v.Errors != null))
                {
                    message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) ?? string.Empty;
                }

                return BadRequest(ex.Message + " " + message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoContacto(int id, TipoContactRequest tipo)
        {
            try 
            {
                if (tipo == null)
                {
                    throw new Exception("el tipo de contacto ingresado es vacio");
                }

                if ( id == 0)
                {
                    throw new Exception("el tipo de contacto ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }

                bool resultadoUpdate = await new ManejadorTipoContacto(_db).update(tipo,id);

                if (resultadoUpdate)
                {
                    return Ok("Tipo de contacto actualizado correctamente!");
                }
                else
                {
                    throw new Exception("Error al actualizar el tipo de contacto");
                }

            }
            catch (Exception ex)
            {
                string message = string.Empty;
                if (ModelState.Values.Any(v => v.Errors != null))
                {
                    message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) ?? string.Empty;
                }

                return BadRequest(ex.Message + " " + message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTp(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("el idtp ingresado es vacio");
                }

                bool resultadoDelete = await new ManejadorTipoContacto(_db).delete(id);

                if (resultadoDelete)
                {
                    return Ok("Tipo de contacto inactivado correctamente!");
                }
                else
                {
                    throw new Exception("Error al inactivar el Tipo de contacto");
                }

            }
            catch (Exception ex)
            {
                string message = string.Empty;
                if (ModelState.Values.Any(v => v.Errors != null))
                {
                    message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) ?? string.Empty;
                }

                return BadRequest(ex.Message + " " + message);
            }
        }
    }
}
