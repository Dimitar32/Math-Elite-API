namespace MathEliteAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Country {  get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender {  get; set; }
        public bool IsGoogleUser { get; set; } = false; // Indicates Google Login
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
