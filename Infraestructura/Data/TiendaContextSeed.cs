using Core.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
namespace Infrastructure.Data;

public class TiendaContextSeed
{
    //Atajo: efseedasync
    public static async Task SeedAsync(TiendaContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            CsvConfiguration myConfig = new CsvConfiguration()
            {
                Delimiter = ","
            };

            //Atajo: efseedmar
            if (!context.Marcas.Any())
            {
                using (var readerMarcas = new StreamReader(ruta + @"/Data/Csvs/marcas.csv"))
                {
                    using (var csvMarcas = new CsvReader(readerMarcas, myConfig))
                    {
                        var marcas = csvMarcas.GetRecords<Marca>();
                        context.Marcas.AddRange(marcas);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Categorias.Any())
            {
                using (var readerCategorias= new StreamReader(ruta + @"/Data/Csvs/categorias.csv"))
                {
                    using (var csvCategorias = new CsvReader(readerCategorias, myConfig))
                    {
                        var categorias = csvCategorias.GetRecords<Categoria>();
                        context.Categorias.AddRange(categorias);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Productos.Any())
            {
                using (var readerProductos = new StreamReader(ruta + @"/Data/Csvs/productos.csv"))
                {
                    using (var csvProductos = new CsvReader(readerProductos, myConfig))
                    {
                        var listadoProductosCsv = csvProductos.GetRecords<Producto>();

                        List<Producto> productos = new List<Producto>();
                        foreach (var item in listadoProductosCsv)
                        {
                            productos.Add(new Producto
                            {
                                Id = item.Id,
                                Nombre = item.Nombre,
                                Precio = item.Precio,
                                FechaCreacion = item.FechaCreacion,
                                CategoriaId = item.CategoriaId,
                                MarcaId = item.MarcaId
                            });
                        }

                        context.Productos.AddRange(productos);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<TiendaContextSeed>();
            logger.LogError(ex.Message);
        }
    }

    //Atajo: efseedrol
    public static async Task SeedRolesAsync(TiendaContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Rol>()
                        {
                            new Rol{Id=1, Nombre="Administrador"},
                            new Rol{Id=2, Nombre="Gerente"},
                            new Rol{Id=3, Nombre="Empleado"},
                        };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<TiendaContextSeed>();
            logger.LogError(ex.Message);
        }
    }

}