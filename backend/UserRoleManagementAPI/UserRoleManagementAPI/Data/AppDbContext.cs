using Microsoft.EntityFrameworkCore;
using UserRoleManagementAPI.Models;

namespace UserRoleManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // Roles
            b.Entity<Role>(e =>
            {
                e.HasIndex(r => r.RoleName).IsUnique();
                e.Property(r => r.RoleName).HasMaxLength(100).IsRequired();
                e.Property(r => r.Description).HasMaxLength(255);
            });

            // Users
            b.Entity<User>(e =>
            {
                e.HasIndex(u => u.Username).IsUnique();
                e.HasIndex(u => u.Email).IsUnique();
                e.Property(u => u.Username).HasMaxLength(100).IsRequired();
                e.Property(u => u.Email).HasMaxLength(255).IsRequired();
                e.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
                e.Property(u => u.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                e.Property(u => u.Status).HasDefaultValue((byte)1);
                e.Property(u => u.RowVersion).IsRowVersion();
                e.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Restrict);
            });

            // Permissions
            b.Entity<Permission>(e =>
            {
                e.HasIndex(p => new { p.RoleId, p.FunctionName }).IsUnique();
                e.Property(p => p.FunctionName).HasMaxLength(100).IsRequired();
                e.Property(p => p.Access).HasDefaultValue(false);
                e.Property(p => p.Create).HasDefaultValue(false);
                e.Property(p => p.Read).HasDefaultValue(false);
                e.Property(p => p.Update).HasDefaultValue(false);
                e.Property(p => p.Delete).HasDefaultValue(false);
                e.Property(p => p.Export).HasDefaultValue(false);
                e.HasOne(p => p.Role).WithMany(r => r.Permissions).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
            });

            // ActivityLogs
            b.Entity<ActivityLog>(e =>
            {
                e.HasIndex(l => new { l.UserId, l.CreatedAt });
                e.Property(l => l.Action).HasMaxLength(255).IsRequired();
                e.Property(l => l.IPAddress).HasMaxLength(64);
                e.Property(l => l.UserAgent).HasMaxLength(255);
                e.Property(l => l.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                e.HasOne(l => l.User).WithMany().HasForeignKey(l => l.UserId).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
