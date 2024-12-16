using Microsoft.EntityFrameworkCore;
using TaskApp.Models;
//using TaskAppDAL;


namespace TaskApp.data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<UserItem> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserItem Configuration
            modelBuilder.Entity<UserItem>()
                .HasKey(u => u.UserId); // Primary Key

            modelBuilder.Entity<UserItem>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<UserItem>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            // TaskItem Configuration
            modelBuilder.Entity<TaskItem>()
                .HasKey(t => t.TaskId); // Primary Key

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Define the relationship
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.UserItem)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes tasks if user is deleted
        }
    }
}
