namespace MathEliteAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int TopicId{ get; set; }
        public Topic Topic { get; set; }
        public string Title { get; set; } // Task title
        public string Expression { get; set; } // Math task like "1 + 1"
        public string Answer { get; set; } // Correct answer
        public string Description { get; set; } // Additional instructions

    }
}
