﻿namespace MathEliteAPI.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User{ get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
    }
}
