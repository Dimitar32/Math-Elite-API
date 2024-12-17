using MathEliteAPI.Data;
using MathEliteAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathEliteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // PUT: api/Users/{id}
        [HttpPut("updateUserDetails/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            // Update user details
            user.BirthDate = userDto.BirthDate;
            user.Gender = userDto.Gender;
            user.Phone = userDto.Phone;
            user.Country = userDto.Country;
            user.UpdatedAt = userDto.UpdatedAt;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "User updated successfully.", user });
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error updating the user.");
            }
        }
    }


}
