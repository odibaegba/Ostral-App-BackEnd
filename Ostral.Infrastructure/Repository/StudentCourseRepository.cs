using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository
{
	public class StudentCourseRepository : IStudentCourseRepository
	{
		private readonly OstralDBContext _context;
		private readonly IMapper _mapper;

		public StudentCourseRepository(OstralDBContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PaginatorResponseDTO<IEnumerable<StudentCourseDTO>>> GetAllStudentCourses(string id, int pageSize, int pageNumber)
		{
			if (_context.Users.Find(id) != null)
				id = _context.Students.Where(s => s.UserId == id).First().Id;
			
			var courses = _context.StudentCourses
				.Where(sc => sc.StudentId == id)
				.Include(sc => sc.Course)
				.Include(sc => sc.Course.Category)
				.Select(sc => new StudentCourseDTO
				{
					CourseId = sc.CourseId,
					CourseName = sc.Course.Name,
					CategoryName = sc.Course.Category.Name,
					CompletionDate = sc.CompletionDate,
					CompletionPercentage = sc.CompletionPercentage,
					ContentCount = sc.Course.ContentList.Count,
					CourseImageUrl = sc.Course.ImageUrl,
					Duration = sc.Course.Duration,
				});

			return await courses.PaginationAsync<StudentCourseDTO, StudentCourseDTO>(pageSize, pageNumber, _mapper);
		}

		public async Task<StudentCourseDTO?> GetStudentCourse(string studentId, string courseId)
		{
			var course = await _context.StudentCourses
				.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
				.Include(sc => sc.Course)
				.Include(sc => sc.Course.Category)
				.Select(sc => new StudentCourseDTO
				{
					CourseId = sc.CourseId,
					CourseName = sc.Course.Name,
					CategoryName = sc.Course.Category.Name,
					CompletionDate = sc.CompletionDate,
					CompletionPercentage = sc.CompletionPercentage,
				}).FirstOrDefaultAsync();

			return course;
		}

		public bool Add(string studentId, string courseId, out string err)
		{
			err = "";
			if (_context.Courses.Find(courseId) == null)
			{
				err = "course does not exist";
				return false;
			}

			if (_context.Users.Find(studentId) != null)
			{
				studentId = _context.Students.FirstOrDefault(u => u.UserId == studentId).Id;
			}

			if (_context.Students.Find(studentId) == null)
			{
				err = "student does not exist";
				return false;
			}

			var isStudentEnrolled = _context.StudentCourses.FirstOrDefault(sc => sc.CourseId == courseId && sc.StudentId == studentId) != null;

			if (isStudentEnrolled)
			{
				err = "student already enrolled";
				return false;
			}

			var studentCourse = new StudentCourse
			{
				StudentId = studentId,
				CourseId = courseId,
			};

			_context.StudentCourses.Add(studentCourse);
			return _context.SaveChanges() > 0;
		}
	}
}