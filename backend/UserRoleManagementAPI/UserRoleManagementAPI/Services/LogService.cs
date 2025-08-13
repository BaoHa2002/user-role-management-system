using UserRoleManagementAPI.Data;
using UserRoleManagementAPI.Models;

namespace UserRoleManagementAPI.Services
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _db;
        public LogService(AppDbContext db) { _db = db; }
        public async Task LogAsync(System.Security.Claims.ClaimsPrincipal user, string action, string? details = null, HttpContext? httpContext = null)
        {
            int? userId = null;
            var sub = user?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (int.TryParse(sub, out var id)) userId = id;
            var log = new ActivityLog
            {
                UserId = userId,
                Action = action,
                Details = details,
                IPAddress = httpContext?.Connection.RemoteIpAddress?.ToString(),
                UserAgent = httpContext?.Request.Headers.UserAgent.ToString(),
                CreatedAt = DateTime.UtcNow
            };
            _db.ActivityLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
