using Microsoft.EntityFrameworkCore;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Domain;

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
        modelBuilder.Entity<TaskModel>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<TaskModel>().Property(x => x.State).IsRequired();
        modelBuilder.Entity<TaskModel>().Property(x => x.DateTimeCreated).IsRequired();
        modelBuilder.Entity<TaskModel>().Property(x => x.Description);
        modelBuilder.Entity<TaskModel>().Property(x => x.CompletedWork);
        modelBuilder.Entity<TaskModel>().Property(x => x.OrginalEstimate);
        modelBuilder.Entity<TaskModel>().Property(x => x.RemainingWork);
    }

}