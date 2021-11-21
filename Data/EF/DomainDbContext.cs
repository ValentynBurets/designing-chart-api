using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.EF
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Admin
            modelBuilder.Entity<Admin>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Attempt
            modelBuilder.Entity<Attempt>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            modelBuilder.Entity<Attempt>()
                .HasOne(i => i.Student)
                .WithMany(a => a.Attempts)
                .HasForeignKey(k => k.StudentId)
                .IsRequired(true);

            modelBuilder.Entity<Attempt>()
                .HasOne(i => i.Exercise)
                .WithMany(a => a.Attempts)
                .HasForeignKey(k => k.ExerciseId)
                .IsRequired(true);

            //Category
            modelBuilder.Entity<CategoryType>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Exercise
            modelBuilder.Entity<Exercise>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.CategoryType)
                .WithMany(c => c.Exercises)
                .HasForeignKey(k => k.CategoryId);

            //Student
            modelBuilder.Entity<Student>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Admin
            modelBuilder.Entity<Admin>()
                .HasIndex(i => i.Id)
                .IsUnique(true);
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Student> Studens { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
    }
}
