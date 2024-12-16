using MathEliteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MathEliteAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Topic> Topics { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Grade and Topic Relationship
        //    modelBuilder.Entity<Grade>()
        //        .HasMany(g => g.Topics)
        //        .WithOne(t => t.Grade)
        //        .HasForeignKey(t => t.GradeId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // Topic and Task Relationship
        //    modelBuilder.Entity<Topic>()
        //        .HasMany(t => t.Tasks)
        //        .WithOne(tsk => tsk.Topic)
        //        .HasForeignKey(tsk => tsk.TopicId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
