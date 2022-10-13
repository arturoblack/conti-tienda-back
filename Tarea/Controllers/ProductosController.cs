using Microsoft.AspNetCore.Mvc;
using Tarea.MyDb.Contexts;
using Tarea.MyDb.Tablas;
using Microsoft.EntityFrameworkCore;

namespace Tarea.Controllers
{
    [Controller]
    [Route("/api/v1/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly MyDbContext db;
        public ProductosController(MyDbContext context)
        {
            db = context;
        }
      //Listar productos
        [HttpGet]
        [Route("")]
        public ActionResult Listar()
        {
            List<Producto> productos = db.Productos.ToList();
            return Ok(productos);

        }
        //listar informacion del producto con el id
        [HttpGet]
        [Route("{id}")]
        public ActionResult ObtenerPorId([FromRoute] int id)
        {
            
            Producto? producto = db.Productos
                .Where(p => p.id == id)
                .FirstOrDefault();

            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado con el id: " + id });

            }
            return Ok(producto);

        }
        //crear un producto
        [HttpPost]
        [Route("")]
        public ActionResult Crear([FromBody] Producto producto)
        {
            db.Productos.Add(producto);
            db.SaveChanges();
            return Ok(producto);

        }
        //actualizar producto
        [HttpPut]
        [Route("{id}")]
        public ActionResult Actualizar([FromRoute] int id, [FromBody] Producto productoDatos)
        {
            Producto? producto = db.Productos
                .Where(p => p.id == id)
                .FirstOrDefault();
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado con el id: " + id });

            }
            producto.codigo = productoDatos.codigo;
            producto.nombre = productoDatos.nombre;
            producto.descripcion = productoDatos.descripcion;
            producto.codigo_barras = productoDatos.codigo_barras;
            producto.precio_venta = productoDatos.precio_venta;

            db.SaveChanges();
            return NoContent();

        }
        //eliminar producto
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Eliminar([FromRoute] int id)
        {
            Producto? producto = db.Productos
                .Where(p => p.id == id)
                .FirstOrDefault();
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado con el id: " + id });

            }
            db.Productos.Remove(producto);
            db.SaveChanges();
            return NoContent();

        }
    }

}
