using Microsoft.EntityFrameworkCore;

namespace toolvana.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.; Database = toolvana; trustServerCertificate= true; trusted_connection=true");
        }
    }
}
