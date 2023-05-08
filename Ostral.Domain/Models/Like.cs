using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Domain.Models
{
    public class Like
    {
        

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int LikeCount { get; set; }
        public string CommentId { get; set; }
        public Comment Comment { get; set; } 
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }
      
        public Like()
        {
            
            
        }

        public Like(string userId, string commentId)
        {
            UserId = userId;
            CommentId = commentId;
        }

       
    }
}
