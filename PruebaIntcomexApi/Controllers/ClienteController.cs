using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Manejadores;
using PruebaIntcomexApi.Models;
using PruebaIntcomexApi.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIntcomexApi.Controllers
{
    [EnableCors("permitir")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        
        public ClienteController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> getClientes() {

            try
            {
                var lista = await new ManejadorCliente(_db).findAll();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getCliente(int id)
        {
            try
            {
                Cliente obj = await new ManejadorCliente(_db).findById(id);
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
        public async Task<IActionResult> guardarCliente([FromBody] ClienteRequest cliente) {

            try
            {
                if (cliente == null)
                {
                    throw new Exception("el cliente ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }

                bool resultadoInsert = await new ManejadorCliente(_db).insert(cliente);

                if (resultadoInsert)
                {
                    return Ok("Cliente guardado correctamente!");
                }
                else 
                {
                    throw new Exception("Cliente no guardado");
                }
            }
            catch (Exception ex)
            {
                string message = string.Empty;
                if (ModelState.Values.Any(v => v.Errors != null)) 
                { 
                    message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) ?? string.Empty;
                }               
                
                return BadRequest(ex.Message+" "+message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteRequest cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new Exception("el cliente ingresado es vacio");
                }

                if (id == 0)
                {
                    throw new Exception("el cliente ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }

                bool resultadoUpdate = await new ManejadorCliente(_db).update(cliente, id);

                if (resultadoUpdate)
                {
                    return Ok("Cliente actualizado correctamente!");
                }
                else
                {
                    throw new Exception("Error al actualizar el usuario");
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
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("el IdCliente ingresado es vacio");
                }

                bool resultadoDelete = await new ManejadorCliente(_db).delete(id);

                if (resultadoDelete)
                {
                    return Ok("Cliente inactivado correctamente!");
                }
                else
                {
                    throw new Exception("Error al inactivar el Cliente");
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
