using Microsoft.AspNetCore.Authorization;

namespace UserRoleManagementAPI.Auth
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        const string PolicyPrefix = "Perm";
        public HasPermissionAttribute(string function, string action)
        {
            Policy = $"{PolicyPrefix}:{function}:{action}";
        }
    }
}
