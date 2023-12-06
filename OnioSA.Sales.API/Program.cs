
using Microsoft.EntityFrameworkCore;
using OnioSA.Sales.API.Persistence;
using OnioSA.Sales.API.Repository.Arquivos;

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
            builder.Services.AddCors();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IArquivosRepository, ArquivosRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}