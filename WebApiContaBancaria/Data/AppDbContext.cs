using Microsoft.EntityFrameworkCore;
using WebApiContaBancaria.Models.ContaBancariaModel;
using WebApiContaBancaria.Models.Transacoes;

namespace WebApiContaBancaria.Data {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            
        }

        public DbSet<ContaBancariaModel> ContasBancarias { get; set; }
        public DbSet<TransacoesModel> Transacoes { get; set; }
    }
}
