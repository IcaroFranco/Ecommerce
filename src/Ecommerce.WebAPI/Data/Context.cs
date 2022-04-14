using Ecommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
