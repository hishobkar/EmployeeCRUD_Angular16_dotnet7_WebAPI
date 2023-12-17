using Microsoft.EntityFrameworkCore;
using WebAPIService.Models;

namespace WebAPIService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Employee> Employees { get; set; }
    }
}
