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
    }
}
