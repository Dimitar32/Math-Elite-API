using MathEliteAPI.Models;

namespace MathEliteAPI.DataTransferObjects
{
    public class CreateGradeDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string SchoolStage { get; set; }
    }
}
