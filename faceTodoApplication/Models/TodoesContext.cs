using Microsoft.EntityFrameworkCore;

namespace faceTodoApplication.Models
{
    public class TodoesContext : DbContext
    {
        public TodoesContext(DbContextOptions<TodoesContext> options)
            : base(options)
        {
        }

        public DbSet<faceTodoApplication.Models.Todo> Todo { get; set; }
    }
}