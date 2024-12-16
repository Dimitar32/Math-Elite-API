using MathEliteAPI.Models;

namespace MathEliteAPI.DataTransferObjects
{
    public class CreateTopicDto
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int GradeId { get; set; }

    }
}
