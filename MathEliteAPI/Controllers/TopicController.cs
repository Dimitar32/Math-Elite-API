using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using MathEliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathEliteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TopicController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllTopics")]
        public async Task<IActionResult> GetAllTopics()
        {
            var topics = await _context.Topics.ToListAsync();
            return Ok(topics);
        }

        [HttpPost("AddTopic")]
        public async Task<IActionResult> AddTopic([FromBody] CreateTopicDto topicDto)
        {
            if (topicDto == null || string.IsNullOrWhiteSpace(topicDto.Title) || string.IsNullOrWhiteSpace(topicDto.Description) || topicDto.GradeId <= 0)
            {
                return BadRequest("Invalid Topic data.");
            }

            var newTopic = new Topic
            {
                Title = topicDto.Title,
                Description = topicDto.Description,
                GradeId = topicDto.GradeId,
                Grade = await _context.Grades.FindAsync(topicDto.GradeId)
            };

            _context.Topics.Add(newTopic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTopicById), new { id = newTopic.Id }, newTopic);
        }

        [HttpGet("GetTopicById")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound(new { message = $"Topic {id} not found" });
            }

            return Ok(topic);
        }

    }
}
