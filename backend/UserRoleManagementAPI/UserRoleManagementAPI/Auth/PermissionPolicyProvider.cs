using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace UserRoleManagementAPI.Auth
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Perm:", StringComparison.OrdinalIgnoreCase))
            {
                var parts = policyName.Split(':');
                var function = parts[1];
                var action = parts[2];
                var policy = new AuthorizationPolicyBuilder()
                  .AddRequirements(new PermissionRequirement(function, action))
                  .Build();
                return Task.FromResult<AuthorizationPolicy?>(policy);
            }
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();
    }
}
