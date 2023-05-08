using Ostral.Core.DTOs;
using Ostral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentById(string commentId);
        Task<Comment> AddCommentAsync(string courseId, CommentDTO comment, User user);
        Task ToggleLikeAsync(string commentId, User user);

        Task<IEnumerable<CommentDTO>> GetAllCommentsAsync(string courseId);
        
        // Task<ICollection<Comment>> GetReplies(string commentId);


        Task<Comment> AddReply(string commentId, CommentDTO reply, User user);
    }
}
