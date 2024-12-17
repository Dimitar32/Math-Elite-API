namespace MathEliteAPI.DataTransferObjects
{
    public class UpdateUserDto
    {
        public string Country { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
