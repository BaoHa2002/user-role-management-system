using Microsoft.EntityFrameworkCore;
using UserRoleManagementAPI.Models;

namespace UserRoleManagementAPI.Data
{
    public class SeedData
    {
        public static async Task EnsureSeededAsync(IServiceProvider sp)
        {
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // 1) Roles
            var roles = new[] {
                ("Admin","Full access"),
                ("Manager","Manage users/roles"),
                ("Editor","CRUD content"),
                ("Viewer","Read only")
            };
            foreach (var (name, desc) in roles)
            {
                if (!await db.Roles.AnyAsync(r => r.RoleName == name))
                {
                    db.Roles.Add(new Role { RoleName = name, Description = desc });
                }
            }
            await db.SaveChangesAsync();

            // 2) Permissions cho Admin (tùy bạn chỉnh danh sách function)
            var admin = await db.Roles.FirstAsync(r => r.RoleName == "Admin");
            var functions = new[] { "Dashboard", "Users", "Roles", "Permissions", "Logs" };
            foreach (var fn in functions)
            {
                var p = await db.Permissions.FirstOrDefaultAsync(x => x.RoleId == admin.Id && x.FunctionName == fn);
                if (p == null)
                {
                    db.Permissions.Add(new Permission
                    {
                        RoleId = admin.Id,
                        FunctionName = fn,
                        Access = true,
                        Create = true,
                        Read = true,
                        Update = true,
                        Delete = true,
                        Export = true
                    });
                }
                else
                {
                    p.Access = p.Create = p.Read = p.Update = p.Delete = p.Export = true;
                }
            }
            await db.SaveChangesAsync();

            // 3) Tạo admin user nếu chưa có
            var adminUsername = Environment.GetEnvironmentVariable("SEED_ADMIN_USERNAME") ?? "admin";
            var adminEmail = Environment.GetEnvironmentVariable("SEED_ADMIN_EMAIL") ?? "admin@example.com";
            var adminPassword = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD") ?? "Admin@123"; // nhớ đổi trong prod

            if (!await db.Users.AnyAsync(u => u.Username == adminUsername))
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(adminPassword);
                db.Users.Add(new User
                {
                    Username = adminUsername,
                    Email = adminEmail,
                    PasswordHash = hash,
                    RoleId = admin.Id,
                    Status = 1,
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
