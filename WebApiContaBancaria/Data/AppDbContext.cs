using Microsoft.EntityFrameworkCore;
using WebApiContaBancaria.Models.ContaBancaria;
using WebApiContaBancaria.Models.Transacoes;

namespace WebApiContaBancaria.Data {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            
        }

        public DbSet<ContaBancaria> ContaBancarias { get; set; }
        public DbSet<Transacoes> Transacoes { get; set; }
    }
}
