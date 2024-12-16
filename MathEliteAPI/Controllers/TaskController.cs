using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
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

        [HttpPost("addNewTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var newTask = new Models.Task
            {
                Grade = dto.Grade,
                Title = dto.Title,
                Expression = dto.Expression,
                Answer = dto.Answer,
                Description = dto.Description
            };

            _context.Tasks.Add(newTask); // Add task to DB
            await _context.SaveChangesAsync();

            return Ok(newTask);
        }

        [HttpGet("getTaskById")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            return Ok(task);
        }

        [HttpGet("getTasksByGrade/{grade}")]
        public async Task<IActionResult> GetTasksByGrade(string grade)
        {
            var tasks = await _context.Tasks
                .Where(t => t.Grade == grade)
                .ToListAsync();

            if (!tasks.Any())
            {
                return NotFound(new { message = "No tasks found for this grade" });
            }

            return Ok(tasks);
        }
    }
}
