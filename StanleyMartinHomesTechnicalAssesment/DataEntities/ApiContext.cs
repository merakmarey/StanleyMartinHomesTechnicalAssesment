using Microsoft.EntityFrameworkCore;
using StanleyMartinHomesTechnicalAssesment.DataEntities.Models;

namespace StanleyMartinHomesTechnicalAssesment.DataEntities
{
    public class ApiContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ProductsDb");
        }
        public DbSet<Metro> Metros { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
