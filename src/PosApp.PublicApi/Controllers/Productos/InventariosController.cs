﻿using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.Models.Dominio.Productos;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Controllers.Productos
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly IRepositorioDeInventarios _repositorioDeInventarios;   

        public InventariosController( IRepositorioDeInventarios repositorioDeInventarios)
        {
            _repositorioDeInventarios = repositorioDeInventarios;
        }

        // GET: api/<InventariosController>
        [HttpGet]
        public async Task<ActionResult<RespuestaDto>> ListaDeInvetarios()
        {
            List<Inventarios> lista = await _repositorioDeInventarios.listeElInventarios();
            var respuesta = new RespuestaDto { Respuesta = lista };
            return  Ok(respuesta);
        }

        // GET api/<InventariosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaDto>> InventarioPorId(int id)
        {
            var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(id);
            if (inventario != null)
            {
                var respuesta = new RespuestaDto() { Respuesta = inventario };
                return Ok(respuesta);
            }
            else return BadRequest("id no encontardo.");
        }

        // GET api/Inventarios/nombre/Lit

        [HttpGet("Nombre")]
        public async Task<ActionResult<List<Inventarios>>> InventariosPorNombre(string Nombre)
        {
            var inventarios = await _repositorioDeInventarios.ListarInventariosPorNombre(Nombre);
            if (inventarios != null)
            {
                var respuesta = new RespuestaDto() { Respuesta = inventarios };
                return Ok(respuesta);
            }
            else return BadRequest("id no encontardo.");

        }


        // POST api/<InventariosController>
        [HttpPost]
        public async Task<ActionResult<Inventarios>> AgregarInventario([FromBody] CrearProductoRequest producto)
        {
            if(ModelState.IsValid)
            {
                Inventarios inventario = new()
                {
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = 0,
                    Categoria =  (Categoria)producto.Categoria
                };
                await _repositorioDeInventarios.AgregarInventario(inventario);
                var inventarios = await _repositorioDeInventarios.ListarInventariosPorNombre(producto.Nombre);
                return Ok(inventarios);
            }else { return BadRequest("Modelo incorrecto"); }
        }

        // PUT api/<InventariosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> EditarInventario(int id, [FromBody] CrearProductoRequest producto)
        {
            if (ModelState.IsValid)
            {
                var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(id);

                if (inventario != null)
                {
                    inventario.Nombre = producto.Nombre;
                    inventario.Precio = producto.Precio;
                    inventario.Categoria = (Categoria)producto.Categoria;

                    await _repositorioDeInventarios.EditarInventario(inventario);

                    return Ok($"Inventario {id} actualizado");
                }
                else return BadRequest("No existe ese inventario");

                
            }
            else { return BadRequest("Modelo incorrecto"); }
        }

        // DELETE api/<InventariosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(id);

            if (inventario != null)
            {
                await _repositorioDeInventarios.EliminarInventario(inventario);
                return Ok($"Inventario {id} eliminado!");

            }
            else return BadRequest("No existe el inventario");
        }
    }
}
