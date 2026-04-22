using Microsoft.EntityFrameworkCore;

namespace TaskManager.Storage
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<ProjectStorageModel> Projects { get; set; }
        public DbSet<TaskStorageModel> Tasks { get; set; }

        public TaskManagerDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=taskmanager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectStorageModel>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}