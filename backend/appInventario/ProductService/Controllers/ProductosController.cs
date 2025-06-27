using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppInventarioContext _context;

        public ProductosController(AppInventarioContext context)
        {
            _context = context;
        }

        // GET /api/productos
        [HttpGet]
        public async Task<IActionResult> GetProductos(
            int pagina = 1,
            int cantidad = 10,
            string? nombre = null,
            string? categoria = null)
        {
            var query = _context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre));
            }
                
            if (!string.IsNullOrEmpty(categoria))
            {
                query = query.Where(p => p.Categoria == categoria);
            }
                
            var total = await query.CountAsync();

            var productos = await query
                .Skip((pagina - 1) * cantidad)
                .Take(cantidad)
                .ToListAsync();

            return Ok(new
            {
                Total = total,
                Página = pagina,
                Cantidad = cantidad,
                Datos = productos
            });
        }

        // GET /api/productos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // POST /api/productos
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
        }

        // PUT /api/productos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest("El ID del producto no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            var existente = await _context.Productos.FindAsync(id);
            if (existente == null)
            {
                return NotFound();
            }
                
            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Categoria = producto.Categoria;
            existente.ImagenUrl = producto.ImagenUrl;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/productos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
              
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT /api/productos/actualizar-stock
        [HttpPut("actualizar-stock")]
        public async Task<IActionResult> ActualizarStock([FromBody] StockUpdateDTO update)
        {
            var producto = await _context.Productos.FindAsync(update.IdProducto);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }
                
            producto.Stock += update.Cantidad;

            if (producto.Stock < 0)
            {
                return BadRequest("Stock insuficiente.");
            }
              
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public class StockUpdateDTO
        {
            public int IdProducto { get; set; }
            public int Cantidad { get; set; } 
        }
        // GET /api/productos - para saber el nombre del producto
        [HttpGet("ids")]
        public async Task<IActionResult> GetProductosByIds([FromQuery] int[] ids)
        {
            var productos = await _context.Productos
                .Where(p => ids.Contains(p.IdProducto))
                .Select(p => new { p.IdProducto, p.Nombre })
                .ToListAsync();

            return Ok(productos);
        }


    }
}
