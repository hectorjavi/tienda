using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class TiendaContext: DbContext
{
    //DbContextOptions contine una cadena de conexion a la DB
    public TiendaContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Marca> Marcas{ get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Refleccion, para poder obtener el elemento que se esta ejecutando -> Assembly.GetExecutingAssembly()
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}