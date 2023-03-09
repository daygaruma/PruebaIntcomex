using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaIntcomexApi.Data;
using PruebaIntcomexApi.Manejadores;
using PruebaIntcomexApi.Models;
using PruebaIntcomexApi.Utilidades;
using Microsoft.AspNetCore.Cors;

namespace PruebaIntcomexApi.Controllers
{
    [EnableCors("permitir")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class CargoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CargoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> getCargos()
        {

            try
            {
                var lista = await new ManejadorCargo(_db).findAll();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{cargo}")]
        public async Task<IActionResult> getCargo(string cargo)
        {
            try
            {
                Cargo obj = await new ManejadorCargo(_db).findByCargo(cargo);
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
        public async Task<IActionResult> guardarCargo([FromBody] CargoRequest cargo)
        {

            try
            {
                if (cargo == null)
                {
                    throw new Exception("el cargo ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }
                Cargo obj = await new ManejadorCargo(_db).findByCargo(cargo.NombreCargo);
                
                if (obj != null)
                {
                    throw new Exception("el cargo ingresado ya existe");
                }

                bool resultadoInsert = await new ManejadorCargo(_db).insert(cargo);

                if (resultadoInsert)
                {
                    return Ok("Cargo guardado correctamente!");
                }
                else
                {
                    throw new Exception("Cargo no guardado");
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
        public async Task<IActionResult> PutCargo(int id, CargoRequest cargo)
        {
            try
            {
                if (cargo == null)
                {
                    throw new Exception("el cargo ingresado es vacio");
                }

                if (id == 0)
                {
                    throw new Exception("el cargo ingresado es vacio");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Modelo de datos invalido");
                }

                bool resultadoUpdate = await new ManejadorCargo(_db).update(cargo, id);

                if (resultadoUpdate)
                {
                    return Ok("Usuario actualizado correctamente!");
                }
                else
                {
                    throw new Exception("Error al actualizar el cargo");
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
        public async Task<IActionResult> DeleteCargo(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("el idcargo ingresado es vacio");
                }

                bool resultadoDelete = await new ManejadorCargo(_db).delete(id);

                if (resultadoDelete)
                {
                    return Ok("Cargo inactivado correctamente!");
                }
                else
                {
                    throw new Exception("Error al inactivar el cargo");
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
