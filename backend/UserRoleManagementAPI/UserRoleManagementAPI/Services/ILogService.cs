using System.Security.Claims;

namespace UserRoleManagementAPI.Services
{
    public interface ILogService
    {
        Task LogAsync(ClaimsPrincipal user, string action, string? details = null, HttpContext? httpContext = null);
    }
}
