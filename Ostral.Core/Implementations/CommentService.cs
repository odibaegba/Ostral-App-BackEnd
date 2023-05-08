using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Core.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Result<Comment>> AddCommentAsync(string courseId,CommentDTO comment, User user)
        {
            var comments = await _commentRepository.AddCommentAsync(courseId,comment,user);
            if(comments == null) return new Result<Comment> { Success = false };
            return new Result<Comment> { Success =true , Data = comments};

        }

        public async Task<Result<Like>> ToggleLikeAsync(string commentId, User user)
        {
            var comment =_commentRepository.ToggleLikeAsync(commentId, user);
            if(comment == null) return new Result<Like> { Success = false };
            return new Result<Like> { Success = true};

        }

        public async Task<Result<Comment>> AddReply(string commentId, CommentDTO reply, User user)
        {
            var replies = await _commentRepository.AddReply(commentId, reply, user);
            if (replies == null) return new Result<Comment> { Success = false };
            return new Result<Comment> { Success = true, Data = replies };
        }

        public async Task<Result<IEnumerable<CommentDTO>>> GetAllCommentsAsync(string courseId)
        {
            var comments = await _commentRepository.GetAllCommentsAsync(courseId);
            if (comments == null) return new Result<IEnumerable<CommentDTO>>
            {
                Success = false,
                Errors = new List<string> { "No Comments found." }
            };

           return new Result<IEnumerable<CommentDTO>> { Success = true, Data = comments};
        }

        public async Task<Result<Comment>> GetCommentById(string commentId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);
            if(comment == null) return new Result<Comment> { Success= false };
            return new Result<Comment> { Success = true, Data = comment };
        }

      

      
    }
}
