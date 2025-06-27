using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionService.Data;
using TransactionService.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace TransactionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly AppInventarioContext _context;
        private readonly IHttpClientFactory _httpClientFactory;  
        private const string PRODUCT_API = "https://localhost:5001/api/productos/ids";

        public TransaccionesController(AppInventarioContext context,
                                       IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // GET /api/transacciones
        [HttpGet]
        public async Task<IActionResult> GetTransacciones(
        int pagina = 1, int cantidad = 10,
        string? tipo = null,
        DateTime? fechaInicio = null,
        DateTime? fechaFin = null)
        {
            var query = _context.Transacciones.AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(t => t.TipoTransaccion == tipo);
            }
                
            if (fechaInicio.HasValue)
            {
                query = query.Where(t => t.Fecha >= fechaInicio.Value);
            }
                
            if (fechaFin.HasValue)
                query = query.Where(t => t.Fecha <= fechaFin.Value);

            var total = await query.CountAsync();

            var paginaDatos = await query
                .OrderByDescending(t => t.Fecha)
                .Skip((pagina - 1) * cantidad)
                .Take(cantidad)
                .ToListAsync();

            var ids = paginaDatos.Select(t => t.IdProducto).Distinct().ToArray();
            if (ids.Length > 0)
            {
                var client = _httpClientFactory.CreateClient();
                var query1 = string.Join("&", ids.Select(id => $"ids={id}"));
                var resp = await client.GetAsync($"{PRODUCT_API}?{query1}");

                if (resp.IsSuccessStatusCode)
                {
                    var productos = await resp.Content.ReadFromJsonAsync<List<ProductoNombreDto>>();

                    var mapa = productos!.ToDictionary(p => p.IdProducto, p => p.Nombre);

                    foreach (var t in paginaDatos)
                        if (mapa.TryGetValue(t.IdProducto, out var nom))
                        {
                            t.nombreProducto = nom;
                        }
      
                }
                else
                {
                    var errorContent = await resp.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al llamar a productos: {resp.StatusCode} - {errorContent}");
                }
            }

            return Ok(new
            {
                Total = total,
                Página = pagina,
                Cantidad = cantidad,
                Datos = paginaDatos
            });
        }


        private record ProductoNombreDto(int IdProducto, string Nombre);


        // GET /api/transacciones/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaccion(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            return Ok(transaccion);
        }

        // POST /api/trasacciones nuevo
        [HttpPost]
        public async Task<IActionResult> CrearTransaccion([FromBody] Transaccion transaccion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (transaccion.TipoTransaccion == "V")
            {
                var client = _httpClientFactory.CreateClient();
                var productoResponse = await client.GetAsync($"https://localhost:5001/api/productos/{transaccion.IdProducto}");
                if (!productoResponse.IsSuccessStatusCode)
                {
                    return BadRequest("Producto no encontrado en ProductService.");
                }

                var producto = await productoResponse.Content.ReadFromJsonAsync<ProductoDTO>();
                if (producto == null)
                {
                    return BadRequest("Error al obtener información del producto.");
                }
                  
                if (producto.Stock < transaccion.Cantidad)
                {
                    return BadRequest("Stock insuficiente para realizar la venta.");
                }
                    
            }

            // Calcular precio total 
            if (transaccion.PrecioTotal <= 0)
            {
                transaccion.PrecioTotal = transaccion.PrecioUnitario * transaccion.Cantidad;
            }
                
            transaccion.Fecha = DateTime.Now;
            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            // Actualizar stock 
            var httpClient = _httpClientFactory.CreateClient();
            var stockUpdate = new
            {
                IdProducto = transaccion.IdProducto,
                Cantidad = (transaccion.TipoTransaccion == "C") ? transaccion.Cantidad : -transaccion.Cantidad
            };
            var updateResponse = await httpClient.PutAsJsonAsync($"https://localhost:5001/api/productos/actualizar-stock", stockUpdate);
            if (!updateResponse.IsSuccessStatusCode)
            {
                return StatusCode(500, "Error al actualizar stock - intente de nuevo");
            }

            return CreatedAtAction(nameof(GetTransaccion), new { id = transaccion.IdTransaccion }, transaccion);
        }

        public class ProductoDTO
        {
            public int IdProducto { get; set; }
            public int Stock { get; set; }
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTransaccion(int id, [FromBody] Transaccion transaccion)
        {
            if (id != transaccion.IdTransaccion)
            {
                return BadRequest("ID de la transacción no coincide.");
            }

            var existente = await _context.Transacciones.FindAsync(id);
            if (existente == null)
            {
                return NotFound();
            }
                
            existente.Fecha = transaccion.Fecha;
            existente.TipoTransaccion = transaccion.TipoTransaccion;
            existente.IdProducto = transaccion.IdProducto;
            existente.Cantidad = transaccion.Cantidad;
            existente.PrecioUnitario = transaccion.PrecioUnitario;
            existente.PrecioTotal = transaccion.PrecioTotal;
            existente.Detalle = transaccion.Detalle;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTransaccion(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            _context.Transacciones.Remove(transaccion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
