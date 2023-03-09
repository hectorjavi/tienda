using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API
{
    public class Program
    {
        static async Task AutoMigrate(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<TiendaContext>();
                    await context.Database.MigrateAsync();
                    await TiendaContextSeed.SeedAsync(context, loggerFactory);
                    await TiendaContextSeed.SeedRolesAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Ocurrio un error durante la migracion");
                }
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

            builder.Services.ConfigureRateLimiting(); // Agregar el servicio al contenedor de dependencias, ademas usar el middleware

            //Agregar Acceso al API desde el cliente
            builder.Services.ConfigureCors();

            builder.Services.AddAplicacionServices();

            builder.Services.ConfigureApiVersioning(); //Configuracion del versionamiento

            builder.Services.AddJwt(builder.Configuration); // Configuracion del JWT


            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;
            }).AddXmlSerializerFormatters();

            //Agregar el siguiente codigo para la conexion a la DB
            builder.Services.AddDbContext<TiendaContext>(options =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            AutoMigrate(app).Wait();

            app.UseIpRateLimiting(); // Si no se agrega este middleaware no funciona

            //Cors Policy
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Para usar el JWT

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}