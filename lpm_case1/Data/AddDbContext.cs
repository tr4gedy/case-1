using lpm_case1.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace lpm_case1.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<ProgressHistory> ProgressHistories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=learning_progress.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.ProgressHistory)
                .WithOne(ph => ph.Course)
                .HasForeignKey(ph => ph.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
