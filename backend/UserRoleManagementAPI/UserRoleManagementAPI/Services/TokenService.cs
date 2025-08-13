using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserRoleManagementAPI.Models;

namespace UserRoleManagementAPI.Services
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public string Key { get; set; } = "";
        public int ExpiresMinutes { get; set; } = 120;
    }

    public class TokenService : ITokenService
    {
        private readonly JwtOptions _opt;
        public TokenService(IOptions<JwtOptions> opt) { _opt = opt.Value; }
        public string CreateToken(User user, IEnumerable<string> permissions)
        {
            var claims = new List<Claim> {
      new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new(ClaimTypes.Name, user.Username),
      new(ClaimTypes.Role, user.Role.RoleName)
    };
            // Thêm permissions vào claim dạng "Function:Action"
            foreach (var p in permissions) claims.Add(new Claim("perm", p));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _opt.Issuer,
              audience: _opt.Audience,
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(_opt.ExpiresMinutes),
              signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
