namespace MathEliteAPI.Models
{
    public class Faq
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; } // Optional
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
