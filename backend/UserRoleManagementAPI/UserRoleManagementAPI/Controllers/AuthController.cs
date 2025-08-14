using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoleManagementAPI.Data;
using UserRoleManagementAPI.Services;

namespace UserRoleManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITokenService _token;

        public AuthController(AppDbContext db, ITokenService token)
        {
            _db = db;
            _token = token;
        }

        //[HttpPost("login")]
        //public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
        //{
        //    var user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == req.Username);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash) || user.Status == 0)
        //        return Unauthorized("Invalid credentials");

        //    var perms = await _db.Permissions
        //      .Where(p => p.RoleId == user.RoleId)
        //      .SelectMany(p => new[] {
        //            p.Access ? $"{p.FunctionName}:Access" : null,
        //            p.Create ? $"{p.FunctionName}:Create" : null,
        //            p.Read   ? $"{p.FunctionName}:Read"   : null,
        //            p.Update ? $"{p.FunctionName}:Update" : null,
        //            p.Delete ? $"{p.FunctionName}:Delete" : null,
        //            p.Export ? $"{p.FunctionName}:Export" : null
        //      }.Where(x => x != null)!)
        //      .ToListAsync();

        //    var token = _token.CreateToken(user, perms!);
        //    return Ok(new LoginResponse(token, user.Username, user.Role.RoleName, perms!));
        //}

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Với JWT stateless, logout do client xóa token. Có thể triển khai blacklist nếu cần.
            return Ok();
        }
    }
}
