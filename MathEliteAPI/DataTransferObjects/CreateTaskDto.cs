using MathEliteAPI.Models;

namespace MathEliteAPI.DataTransferObjects
{
    public class CreateTaskDto
    {
        public int TopicId { get; set; } // Class level (e.g., "1")
        public string Title { get; set; } // Task title
        public string Expression { get; set; } // Math task like "1 + 1"
        public string Answer { get; set; } // Correct answer
        public string Description { get; set; } // Additional instructions
    }
}
