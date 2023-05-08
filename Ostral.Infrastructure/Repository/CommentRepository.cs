using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;
using System.Security.Claims;

namespace Ostral.Infrastructure.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly OstralDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IStudentCourseService _courseService;

        public CommentRepository(OstralDBContext dbContext, IMapper mapper, IStudentCourseService courseService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _courseService = courseService;
        }

        public async Task<Comment> AddCommentAsync(string courseId, CommentDTO comment, User user)
        {

            string userId = user.Id;
            var enroll = _courseService.GetStudentCourseById(userId, courseId);
            var comments = _mapper.Map<Comment>(comment);
            var course = await _dbContext.Courses.FirstOrDefaultAsync( c => c.Id== courseId );
            if (course == null)
            {
                return null;
            }
            else if (enroll == null)
            {
                return null;
            }

            var newComment = new Comment(userId, courseId)
            {
                Text = comment.Text,
                CreatedAt = DateTime.Now,
                CourseId= courseId,
                UserId= userId
                
               
                
            };

            course.Comments.Add(newComment);
            await _dbContext.SaveChangesAsync();
            return newComment;
        }

        public async Task ToggleLikeAsync(string commentId, User user)
        {
         
            string userId = user.Id;
            var existingLike = _dbContext.Likes.SingleOrDefault(l => l.UserId == userId && l.CommentId == commentId);
           

            if (existingLike != null)
            {
                _dbContext.Likes.Remove(existingLike);
                existingLike.LikeCount--;
               

            }

            else
            {


                var newLike = new Like
                {
                    CommentId = commentId,
                    UserId = user.Id
                };

                await _dbContext.Likes.AddAsync(newLike);
                newLike.LikeCount++;
               
            }
            await _dbContext.SaveChangesAsync();

           

        }

        public async Task<Comment> AddReply(string commentId, CommentDTO reply, User user)
        {
            var comment = new Comment
            {
                ParentCommentId = commentId,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                Text = reply.Text,
            };
            //var replies = _mapper.Map<Comment>(reply);
            //var parentComment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            //if (parentComment == null)
            //{
            //    return null!;
            //}

            //commentId = replies.Id;
            //string userId = user.Id;
            //var newComment = new Comment(userId, commentId)
            //{
            //    Text = reply.Text,
            //    CreatedAt = DateTime.Now,
            //    ParentCommentId = commentId,
            //    UserId = userId  

            //};
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentsAsync(string courseId)
        {
            var comments = _dbContext.Comments.Select(c => new CommentDTO
            {
                //Id = c.Id,
                CreatedAt = c.CreatedAt,
                Text = c.Text,
            });

            return await comments.ToListAsync();
                   //.Where(c => c.CourseId == courseId != null)
                   //.Include(c => c.Likes)
                   //.ToListAsync();

            
        }

       
        public async Task<Comment?> GetCommentById(string commentId)
        {

            return await _dbContext.Comments
                             .Include(c => c.Likes)
                             .FirstOrDefaultAsync(c => c.Id == commentId);

        }
        

    }
}
