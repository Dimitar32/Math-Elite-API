using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using MathEliteAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MathEliteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CertificateController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("by-user")]
        public async Task<IActionResult> GetCertificatesByUser()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email is missing in the token.");
            }

            Console.WriteLine($"Authenticated Email: {email}");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var certificates = await _context.Certificates
                                             .Where(c => c.UserId == user.Id)
                                             .ToListAsync();

            return Ok(certificates);
        }

        [HttpGet("GetAllCertificates")]
        public async Task<IActionResult> GetAllCertificates()
        {
            var certificates = await _context.Certificates.ToListAsync();
            return Ok(certificates);
        }

        [HttpGet("GetCertificatesByUserId/{userId}")]
        public async Task<IActionResult> GetCertificatesByUser(int userId)
        {
            var certificates = await _context.Certificates.ToListAsync();

            var tasks = await _context.Certificates
                .Include(c => c.User) 
                .Where(c => c.User.Id == userId)
                .ToListAsync();

            return Ok(certificates);
        }


        [HttpPost("AddCertificate")]
        public async Task<IActionResult> AddCertificate([FromBody] CreateCertificateDto certificateDto)
        {
            if (certificateDto == null || string.IsNullOrWhiteSpace(certificateDto.Title) || string.IsNullOrWhiteSpace(certificateDto.Description) || certificateDto.UserId <= 0)
            {
                return BadRequest("Invalid Certificate data.");
            }

            var newCertificate = new Certificate
            {
                Title = certificateDto.Title,
                Description = certificateDto.Description,
                UserId = certificateDto.UserId,
                User = await _context.Users.FindAsync(certificateDto.UserId)
            };

            _context.Certificates.Add(newCertificate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCertificatesById), new { id = newCertificate.Id }, newCertificate);
        }

        [HttpGet("GetCertificatesById")]
        public async Task<IActionResult> GetCertificatesById(int id)
        {
            var certificate = await _context.Certificates
                .Include(c => c.User) 
                .Where(c => c.Id == id)
                .ToListAsync();

            if (certificate == null)
            {
                return NotFound(new { message = "Certificate not found" });
            }

            return Ok(certificate);
        }


    }
}
