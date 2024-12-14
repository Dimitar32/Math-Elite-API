namespace MathEliteAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; } // e.g., Google, Facebook
        public string ProviderId { get; set; } // Unique ID from the provider
    }
}
