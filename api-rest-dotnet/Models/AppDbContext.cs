using Microsoft.EntityFrameworkCore;

namespace api_rest_dotnet.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Veiculo> Viculos { get; set; }

        public DbSet<Consumo> Consumos { get; set; }
    }
}
