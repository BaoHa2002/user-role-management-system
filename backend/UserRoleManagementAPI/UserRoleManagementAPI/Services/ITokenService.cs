using UserRoleManagementAPI.Models;

namespace UserRoleManagementAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user, IEnumerable<string> permissions);
    }
}
