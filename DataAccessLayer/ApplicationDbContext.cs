using Microsoft.EntityFrameworkCore;
using todoList.Models;

namespace todoList.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
