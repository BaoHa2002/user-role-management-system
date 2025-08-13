using Microsoft.AspNetCore.Authorization;

namespace UserRoleManagementAPI.Auth
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Function { get; }
        public string Action { get; }

        public PermissionRequirement(string function, string action)
        { 
            Function = function;
            Action = action;
        }
    }
}
