using Microsoft.EntityFrameworkCore;
using OnioSA.Sales.API.Entities;
using System.Reflection.Emit;

namespace OnioSA.Sales.API.Persistence
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
            
        }

        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pedidos>(e =>
            {
                e.HasKey(ped => ped.Id);

                e.Property(ped => ped.Id)
                   .HasDefaultValueSql("NEWID()");

                e.Property(ped => ped.Documento)
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)"); 

                e.Property(ped => ped.CEP)
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)");
                
                e.Property(ped => ped.RazaoSocial)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                e.Property(ped => ped.Produto)
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

            });

            builder.Entity<Cliente>(e =>
            {
                e.HasKey(c => c.Documento);
            });


            builder.Entity<Produto>(e =>
            {
                e.HasKey(prod => prod.Id);

                e.Property(prod => prod.Id)
                    .HasDefaultValueSql("NEWID()");

                e.Property(prod => prod.Valor)
                    .HasColumnType("decimal(10,2)");
            });
                
        }
    }
}
