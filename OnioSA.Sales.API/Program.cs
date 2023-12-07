
using Microsoft.EntityFrameworkCore;
using OnioSA.Sales.API.Persistence;
using OnioSA.Sales.API.Repository.Arquivos;
using OnioSA.Sales.API.Repository.Pedidos;

namespace OnioSA.Sales.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<SalesDbContext>(o => o.UseInMemoryDatabase("SalesDb"));

            builder.Services.AddDbContext<SalesDbContext>(o => o.UseSqlServer(conn));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IArquivosRepository, ArquivosRepository>();
            builder.Services.AddScoped<IPedidosRepository, PedidosRepository>();

            #region [Cors]
            builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            #region [Cors]
            app.UseCors("CorsPolicy");
            #endregion

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}