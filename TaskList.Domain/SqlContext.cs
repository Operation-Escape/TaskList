using Microsoft.EntityFrameworkCore;

namespace TaskList.Domain;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Model.Task> Tasks { get; set; }
}