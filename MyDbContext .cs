using academ_sync_back.Models;
using Microsoft.EntityFrameworkCore;

namespace academ_sync_back
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
       .Property(u => u.CreatedAt)
       .HasDefaultValueSql("CURRENT_TIMESTAMP");


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            


            modelBuilder.Entity<Student>()
               .HasOne(s => s.User)
               .WithMany()
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
              .HasOne(t => t.User)
              .WithMany()
              .HasForeignKey(t => t.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Course>()
                .Property(c => c.Type)
                .HasConversion<string>();

            modelBuilder
                .Entity<Course>()
                .Property(c => c.Semester)
                .HasConversion<string>();

           
        }
    }

    
}
