using FullStack.API.Models;
using FullStack.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext()
        {

        }
        public FullStackDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
