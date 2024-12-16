using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using MathEliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathEliteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddNewTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var newTask = new Models.Task
            {
                Topic = await _context.Topics.FindAsync(dto.TopicId),
                TopicId = dto.TopicId,
                Title = dto.Title,
                Expression = dto.Expression,
                Answer = dto.Answer,
                Description = dto.Description
            };

            _context.Tasks.Add(newTask); // Add task to DB
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
        }

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Topic) // Eagerly load the Topic property
                .ThenInclude(topic => topic.Grade) // Optionally include Grade if needed
                .Where(t => t.Id == id)
                .ToListAsync();

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            return Ok(task);
        }

        [HttpGet("GetTasksByGrade/{gradeNumber}")]
        public async Task<IActionResult> GetTasksByGrade(int gradeNumber)
        {
            var tasks = await _context.Tasks
                .Include(t => t.Topic) // Eagerly load the Topic property
                .ThenInclude(topic => topic.Grade) // Optionally include Grade if needed
                .Where(t => t.Topic.Grade.Number == gradeNumber)
                .ToListAsync();

            //if (!tasks.Any())
            //{
            //    return NotFound(new { message = "No tasks found for this grade" });
            //}

            return Ok(tasks);
        }
    }
}
