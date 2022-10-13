using Microsoft.AspNetCore.Mvc;
using Tarea.MyDb.Contexts;
using Tarea.MyDb.Tablas;
using Microsoft.EntityFrameworkCore;

namespace Tarea.Controllers
{
    [Controller]
    [Route("/categorias")]
    public class CategoriaController: ControllerBase    
    {
        private readonly MyDbContext db;

        public CategoriaController(MyDbContext context)
        {
            db = context;
        }
        /////////////////////////////////////////////////////////////////////////
        //lista todas las categorias
        [HttpGet]
        [Route("")]
        public ActionResult Listar()
        {
            List<Categoria> categorias = db.Categoria.ToList();
            return Ok(categorias);
        }
        /////////////////////////////////////////////////////////////////////////
        //crea nueva categoria
        [HttpPost]
        [Route("")]
        public ActionResult Crear([FromBody] Categoria categoria)
        {
            db.Categoria.Add(categoria);
            db.SaveChanges();
            return Ok(categoria);

        }
        /////////////////////////////////////////////////////////////////////////
        //Editar la categoria con la identificador {id}
        [HttpPut]
        [Route("{categoriaId}")]
        public ActionResult Actualizar([FromRoute] int categoriaId,
            [FromBody] Categoria categoriaDatos)
        {
            Categoria? categoria = db.Categoria
                   .Where(a => a.id == categoriaId)
                   .FirstOrDefault();
            if (categoria == null)
            {
                return NotFound(new { message = "Categoria no encontrada" });
            }
            categoria.nombre = categoriaDatos.nombre;
            categoria.categoria_padre = categoriaDatos.categoria_padre;

            db.SaveChanges();
            return NoContent();

        }
        /////////////////////////////////////////////////////////////////////////
        //Eliminar la categoria con la identificador {id}

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Eliminar([FromRoute] int categoriaId)
        {
            Categoria? categoria = db.Categoria
                .Where(p => p.id == categoriaId)
                .FirstOrDefault();
            if (categoria == null)
            {
                return NotFound(new { message = "Categoria no encontrado" });

            }
            db.Categoria.Remove(categoria);
            db.SaveChanges();
            return NoContent();

        }
        /////////////////////////////////////////////////////////////////////////
        //Lista todos los productos de la categoria con identificador {id}

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
        /////////////////////////////////////////////////////////////////////////
    }

}
