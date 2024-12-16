namespace MathEliteAPI.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        //public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
