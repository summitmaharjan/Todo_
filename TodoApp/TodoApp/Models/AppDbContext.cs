using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todoitem> TodoItems { get; set; }
    }
}
