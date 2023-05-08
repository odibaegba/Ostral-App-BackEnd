using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Implementations;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;
using Ostral.Infrastructure;
using System.Security.Claims;

namespace Ostral.API.Controllers
{
    [Route("api/{courseId}/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly OstralDBContext _dbContext;


        public CommentController(ICommentService commentService, OstralDBContext dBContext)
        {
            _commentService = commentService;
            _dbContext = dBContext;
        }

        [HttpPost("addcomment")]

        public async Task<IActionResult> AddComment(string courseId,CommentDTO comment, string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Id == userId);
            var result = await _commentService.AddCommentAsync(courseId, comment, user!);
             return Ok(ResponseDTO<Comment>.Success(result.Data!));
            
        }

        [HttpPost("commentId/like")]
        
        public async Task<IActionResult> ToggleLike(string commentId, string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Id == userId);
            
            var result = await _commentService.ToggleLikeAsync(commentId, user!);
            return Ok(ResponseDTO<Like>.Success(result.Data!));
        }

        [HttpPost("reply")]
        public async Task<IActionResult> AddReply(string commentId, CommentDTO reply, string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Id == userId);
            var result = await _commentService.AddReply(commentId, reply,user!);
            return Ok(ResponseDTO<Comment>.Success(result.Data!));
        }

        [HttpGet("getallcomments")]

        public async Task<IActionResult> GetAllCommentsAsync(string courseId)
        {
            var result = await _commentService.GetAllCommentsAsync(courseId);
           if(result.Success) return Ok(ResponseDTO<IEnumerable<CommentDTO>>.Success(result.Data!));
            return NotFound(ResponseDTO<Comment>.Fail(new[] { "comments not found" }));

        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetCommentById(string commentId)
        {
            var result = await _commentService.GetCommentById(commentId);
            if(result.Success) return Ok(ResponseDTO<Comment>.Success(result.Data!));
            return NotFound(ResponseDTO<Comment>.Fail(new[] { "comment not found" }));
        }

       

      
    }
}
