using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using MathEliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathEliteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GradeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllGrades")]
        public async Task<IActionResult> GetAllGrades()
        {
            var grades = await _context.Grades.ToListAsync();
            return Ok(grades);
        }

        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGrade([FromBody] CreateGradeDto gradeDto)
        {
            if (gradeDto == null || string.IsNullOrWhiteSpace(gradeDto.SchoolStage) || string.IsNullOrWhiteSpace(gradeDto.Name) || gradeDto.Number <= 0)
            {
                return BadRequest("Invalid Grade data.");
            }

            var newGrade = new Grade
            {
                Name = gradeDto.Name,
                SchoolStage = gradeDto.SchoolStage,
                Number = gradeDto.Number
            };

            _context.Grades.Add(newGrade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGradeById), new { id = newGrade.Id }, newGrade);
        }

        [HttpGet("GetGradeById")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _context.Grades.FindAsync(id);

            if (grade == null)
            {
                return NotFound(new { message = $"Grade {id} not found" });
            }

            return Ok(grade);
        }

        [HttpGet("GetGradeByGradeNumber")]
        public async Task<IActionResult> GetGradeByGradeNumber(int gradeNumber)
        {
            var grade = await _context.Grades.Where(t => t.Number == gradeNumber)
                .ToListAsync(); ;

            if (grade == null)
            {
                return NotFound(new { message = $"Grade with number {gradeNumber} not found" });
            }

            return Ok(grade);
        }


    }
}
