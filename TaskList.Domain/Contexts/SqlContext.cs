using Microsoft.EntityFrameworkCore;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Domain.Contexts;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskModel>().HasKey(t => t.Id);
        modelBuilder.Entity<TaskModel>().Property(x => x.Id).ValueGeneratedOnAdd();
    }

}