using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using MathEliteAPI.Models; // Replace with your namespace
using MathEliteAPI.Data; // Replace with your DbContext namespace

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleLoginController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public GoogleLoginController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;

            try
            {
                // Decode and validate the token
                jwtToken = handler.ReadJwtToken(request.IdToken);
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Invalid Google token.");
                }

                // Check if user already exists
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser == null)
                {
                    // Create a new user if not found
                    var newUser = new User
                    {
                        FullName = fullName,
                        Email = email,
                        IsGoogleUser = true, // Custom field to differentiate Google users
                        CreatedAt = DateTime.UtcNow
                    };

                    _dbContext.Users.Add(newUser);
                    await _dbContext.SaveChangesAsync();

                    return Ok(new
                    {
                        Message = "New user created",
                        UserId = newUser.Id,
                        User = newUser
                    });
                }

                // User exists, return the existing user
                return Ok(new
                {
                    Message = "User logged in successfully",
                    UserId = existingUser.Id,
                    User = existingUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Invalid token", Exception = ex.Message });
            }
        }
    }

    public class GoogleLoginRequest
    {
        public string IdToken { get; set; }
    }
}
