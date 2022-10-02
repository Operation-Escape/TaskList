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
        modelBuilder.Entity<TaskModel>().Property(x => x.Name).HasDefaultValue(string.Empty).IsRequired();
        modelBuilder.Entity<TaskModel>().Property(x => x.State).HasDefaultValue(0).IsRequired();
        modelBuilder.Entity<TaskModel>().Property(x => x.DateTimeCreated).IsRequired();
        modelBuilder.Entity<TaskModel>().Property(x => x.Description).HasDefaultValue(string.Empty);
        modelBuilder.Entity<TaskModel>().Property(x => x.CompletedWork).HasDefaultValue(0M);
        modelBuilder.Entity<TaskModel>().Property(x => x.OrginalEstimate).HasDefaultValue(0M);
        modelBuilder.Entity<TaskModel>().Property(x => x.RemainingWork).HasDefaultValue(0M);
    }

}