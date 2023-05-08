using Ostral.Domain.Interfaces;

namespace Ostral.Domain.Models
{
    public class Course : IEntity, IAuditable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Duration { get; set; }
        public decimal Percentage { get; set; }
        public DateTime? Completed { get; set; }


        public string TutorId { get; set; } = string.Empty;
        public Tutor Tutor { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public Category Category { get; set; }
        public ICollection<Content> ContentList { get; set; }
        public ICollection<StudentCourse> StudentList { get; set; }
        public ICollection<Comment> Comments {get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}