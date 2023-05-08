using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Domain.Models
{
    public class Comment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<Like> Likes { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string ParentCommentId { get; set; } = string.Empty;


        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }

        public Comment()
        {
        }

        public Comment(string userId, string courseId)
        {
            UserId = userId;
            CourseId = courseId;
            Likes = new HashSet<Like>();
        }
    }
}