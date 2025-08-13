using Microsoft.AspNetCore.Authorization;

namespace UserRoleManagementAPI.Auth
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var claimValues = context.User.FindAll("perm").Select(c => c.Value);
            var needed = $"{requirement.Function}:{requirement.Action}";
            if (claimValues.Contains(needed) || claimValues.Contains($"{requirement.Function}:Access"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
