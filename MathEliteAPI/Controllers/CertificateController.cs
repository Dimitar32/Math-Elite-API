using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using MathEliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetAllCertificates")]
        public async Task<IActionResult> GetAllCertificates()
        {
            var certificates = await _context.Certificates.ToListAsync();
            return Ok(certificates);
        }

        [HttpGet("GetCertificatesByUser")]
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
