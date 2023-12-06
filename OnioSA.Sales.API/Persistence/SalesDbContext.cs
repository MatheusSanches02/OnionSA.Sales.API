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
        public DbSet<Arquivo> Arquivo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pedidos>(e =>
            {
                e.HasKey(ped => ped.CodPedido);

                e.Property(ped => ped.CodPedido)
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

                e.Property(ped => ped.Prazo)
                    .IsRequired(false);
                
                e.Property(ped => ped.Frete)
                    .IsRequired(false);

            });

            builder.Entity<Cliente>(e =>
            {
                e.HasKey(c => c.Documento);
            });


            builder.Entity<Produto>(e =>
            {
                e.HasKey(prod => prod.CodProduto);

                e.Property(prod => prod.CodProduto)
                    .HasDefaultValueSql("NEWID()");

                e.Property(prod => prod.Valor)
                    .HasColumnType("decimal(10,2)");
            });

            builder.Entity<Arquivo>(e =>
            {
                e.HasKey(arq => arq.CodArquivo);
                e.Property(arq => arq.CodArquivo)
                    .HasDefaultValueSql("NEWID()");
            });
                
        }
    }
}
