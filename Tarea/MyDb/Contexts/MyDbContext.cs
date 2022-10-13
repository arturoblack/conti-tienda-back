using Microsoft.EntityFrameworkCore;
using Tarea.MyDb.Tablas;

namespace Tarea.MyDb.Contexts
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options) { }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCaracteristica> ProductoCaracteristica { get; set; }


    }
}
