using Ostral.Core.DTOs;
using Ostral.Core.Results;
using Ostral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Core.Interfaces
{
    public interface ICommentService
    {
        Task<Result<Comment>> GetCommentById(string commentId);
        Task<Result<Comment>> AddCommentAsync(string courseId,CommentDTO comment, User user);
        Task<Result<Like>> ToggleLikeAsync(string commentId, User user);

       
        Task<Result<IEnumerable<CommentDTO>>> GetAllCommentsAsync(string courseId);
       
        // Task<ICollection<Comment>> GetReplies(string commentId);



        Task<Result<Comment>> AddReply(string commentId, CommentDTO reply, User user);
    }
}
