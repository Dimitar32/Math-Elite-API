namespace MathEliteAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string SchoolStage { get; set; }
        //public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}
