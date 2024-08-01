using iFredCloud.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace iFredCloud.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      public DbSet<User> Users { get; set; }
      public DbSet<UserToken> UsersTokens { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<UserToken>()
            .ToTable("users_tokens")
            .HasKey(t => t.jwt_token);

         base.OnModelCreating(modelBuilder);
      }
   }
}
