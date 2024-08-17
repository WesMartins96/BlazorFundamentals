
using EcommerceStart.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStart.Shared.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Produto> Produtos { get; set; }

    }
}
