using iFredCloud.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace iFredCloud.Data
{
   public class AppDbContext : DbContext
   {
      public DbSet<User> Users { get; set; }
      public DbSet<License> Licenses { get; set; }

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         // Configuração da tabela Users
         modelBuilder.Entity<User>(entity =>
         {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);

            entity.Property(u => u.Username).IsRequired(false).HasMaxLength(60);
            entity.HasIndex(u => u.Username).IsUnique();

            entity.Property(u => u.Email).IsRequired().HasMaxLength(255)
               .HasConversion(
                  v => v.ToLower(),   // Garantir que o e-mail esteja em minúsculas
                  v => v);            // Não alterando ao ler de volta;
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.BirthdayDate).IsRequired(false).HasColumnType("DATE");
            entity.Property(u => u.Cellphone).IsRequired(false).HasMaxLength(20);
            entity.Property(u => u.Telephone).IsRequired(false).HasMaxLength(20);
            entity.Property(u => u.Country).IsRequired(false).HasMaxLength(60);
            entity.Property(u => u.City).IsRequired(false).HasMaxLength(60);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.IsAdmin).IsRequired().HasDefaultValue(0);
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
         });

         // Configuração da tabela Licenses
         modelBuilder.Entity<License>(entity =>
         {
            entity.HasKey(l => l.LicenseId);
            entity.Property(l => l.ServiceName).IsRequired().HasMaxLength(50);
            entity.Property(l => l.LicenseType).IsRequired().HasMaxLength(20);
            entity.Property(l => l.ExpirationDate).IsRequired(false);
            entity.Property(l => l.MaxQuota).IsRequired(false);
            entity.Property(l => l.UsageCount).HasDefaultValue(0);

            // Relacionamento com Users
            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
         });
      }
   }
}
