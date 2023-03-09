using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Manejadores;
using PruebaIntcomexApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaIntcomexApi.Utilidades;
using Microsoft.AspNetCore.Cors;

namespace PruebaIntcomexApi.Controllers
{
    [EnableCors("permitir")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UsuarioController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> getUsuarios()
        {
            try
            {
                var lista = await new ManejadorUsuario(_db).findAll();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{user}")]
        public async Task<IActionResult> getUsuario(int user)
        {
            try
            {
                Usuario obj = await new ManejadorUsuario(_db).findByID(user);
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
        public async Task<IActionResult> guardarUsuario([FromBody] UsuarioRequest usuario)
        {

            try
            {
                if (usuario == null)
                {
                    throw new Exception("el usuario ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }
                Usuario obj = await new ManejadorUsuario(_db).findByUser(usuario.NombreUsuario);
                
                if (obj != null) 
                {
                    throw new Exception("el usuario ingresado ya existe");
                }

                bool resultadoInsert = await new ManejadorUsuario(_db).insert(usuario);

                if (resultadoInsert)
                {
                    return Ok("Usuario guardado correctamente!");
                }
                else
                {
                    throw new Exception("Usuario no guardado");
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
        public async Task<IActionResult> PutUser(int id, UsuarioRequest user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("el usuario ingresado es vacio");
                }

                if (id == 0)
                {
                    throw new Exception("el usuario ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }

                bool resultadoUpdate = await new ManejadorUsuario(_db).update(user, id);

                if (resultadoUpdate)
                {
                    return Ok("Usuario actualizado correctamente!");
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("el Idusuario ingresado es vacio");
                }

                bool resultadoDelete = await new ManejadorUsuario(_db).delete(id);

                if (resultadoDelete)
                {
                    return Ok("Usuario inactivado correctamente!");
                }
                else
                {
                    throw new Exception("Error al inactivar el usuario");
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
