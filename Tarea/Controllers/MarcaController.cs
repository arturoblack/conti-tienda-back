using Microsoft.AspNetCore.Mvc;
using Tarea.MyDb.Contexts;
using Tarea.MyDb.Tablas;
using Microsoft.EntityFrameworkCore;

namespace Tarea.Controllers
{
    [Controller]
    [Route("/marca")]
    public class MarcaController : ControllerBase
    {
        private readonly MyDbContext db;

        public MarcaController(MyDbContext context)
        {
            db = context;
        }

        //listar las marcas
        [HttpGet]
        [Route("")]
        public ActionResult Listar()
        {
            List<Marca> marca = db.Marca.ToList();
            return Ok(marca);
        }

        //listar la marca segun su id
        [HttpGet]
        [Route("{marcaId}")]
        public ActionResult ObtenerPorId([FromRoute] int marcaId)
        {
            Marca? marca = db.Marca
                .Where(a => a.id == marcaId)
                .FirstOrDefault();
            if (marca == null)
            {
                return NotFound(new { message = "Marca no encontrada" });
            }
            return Ok(marca);
        }

        //crear una marca
        [HttpPost]
        [Route("")]
        public ActionResult Crear([FromBody] Marca marca)
        {
            db.Marca.Add(marca);
            db.SaveChanges();
            return Ok(marca);

        }

        //PUT DE ACTUALIZAR(ACTUALIZA SEGUN EL ID INDICADO)

        [HttpPut]
        [Route("{marcaId}")]
        public ActionResult Actualizar([FromRoute] int marcaId,
            [FromBody] Marca marcaDatos)
        {
            Marca? marca = db.Marca
                   .Where(a => a.id == marcaId)
                   .FirstOrDefault();
            if (marca == null)
            {
                return NotFound(new { message = "Marca no encontrada" });
            }
            marca.nombre = marcaDatos.nombre;
            marca.descripcion = marcaDatos.descripcion;
            marca.logo_url = marcaDatos.logo_url;
            db.SaveChanges();
            return NoContent();

        }

        //PUT DE Eliminar(SEGUN EL ID INDICADO)
        [HttpDelete]
        [Route("{marcaId}")]
        public ActionResult Eliminar([FromRoute] int marcaId)
        {
            Marca? marca = db.Marca
                   .Where(a => a.id == marcaId)
                   .FirstOrDefault();
            if (marca == null)
            {
                return NotFound(new { message = "Marca no encontrada" });
            }
            db.Marca.Remove(marca);
            db.SaveChanges();
            return NoContent();
        }
        //identificador id
        [HttpGet]
        [Route("{id}/productos")]

        public ActionResult ListarOne([FromRoute] int id)
        {

            var producto = db.Productos
                .Where(p => p.categoria_id == id);
            if (producto == null)
            {
                return NotFound(new { message = "No puedes listar los productos con el id: " + id });

            }

            List<Producto> productos = db.Productos.ToList();
            return Ok(producto);
        }
    }
}
