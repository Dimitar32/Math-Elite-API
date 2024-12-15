using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using MathEliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathEliteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FaqController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAllFAQ")]
        public async Task<IActionResult> GetAllFaqs()
        {
            var faqs = await _context.Faqs.ToListAsync();
            return Ok(faqs);
        }

        [HttpPost("AddFAQ")]
        public async Task<IActionResult> AddFaq([FromBody] CreateFaqDto faqDto)
        {
            if (faqDto == null || string.IsNullOrWhiteSpace(faqDto.Question) || string.IsNullOrWhiteSpace(faqDto.Answer))
            {
                return BadRequest("Invalid FAQ data.");
            }

            var newFaq = new Faq
            {
                Question = faqDto.Question,
                Answer = faqDto.Answer,
                Category = faqDto.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Faqs.Add(newFaq);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFaqById), new { id = newFaq.Id }, newFaq);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFaqById(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return Ok(faq);
        }
    }
}
