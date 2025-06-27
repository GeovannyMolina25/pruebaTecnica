using Microsoft.EntityFrameworkCore;
using TransactionService.Models;

namespace TransactionService.Data
{
    public class AppInventarioContext : DbContext
    {
        public AppInventarioContext(DbContextOptions<AppInventarioContext> options)
            : base(options) { }

        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>().ToTable("Transacciones");
        }
    }
}
