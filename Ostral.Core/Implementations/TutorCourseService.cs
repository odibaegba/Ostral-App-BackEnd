using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations;

public class TutorCourseService : ITutorCourseService
{
	private readonly ITutorCourseRepository _tutorCourseRepository;
	private readonly ICategoryRepository _categoryRepository;
	private readonly Cloudinary _cloudinary;

	public TutorCourseService(ITutorCourseRepository tutorCourseRepository, ICategoryRepository categoryRepository, Cloudinary cloudinary)
	{
		_tutorCourseRepository = tutorCourseRepository;
		_categoryRepository = categoryRepository;
		_cloudinary = cloudinary;

	}

	public async Task AddCourseAsync(CreateCourseDTO data, string tutorId, string categoryId, IFormFile file)
	{
		var uploadParams = new ImageUploadParams()
		{
			File = new FileDescription(file.FileName, file.OpenReadStream())
		};
		var uploadResult = _cloudinary.Upload(uploadParams);
		//var publicId = uploadResult.PublicId;
		var imageUrl = uploadResult.SecureUrl.AbsoluteUri;
		var course = new Course()
		{
			Name = data.Name,
			Description = data.Description,
			ImageUrl = imageUrl,
			Price = data.Price,
			Category = await _categoryRepository.GetCategoryById(categoryId),
			Duration = 0,
			TutorId = tutorId
		};
		await _tutorCourseRepository.AddCourseAsync(course);
	}

	public async Task<TutorCourseResult> GetAllTutorCourses(string tutorId, int pageSize, int pageNumber)
	{
		var tutorCourses = await _tutorCourseRepository.GetCourses(tutorId, pageSize, pageNumber);

		return new TutorCourseResult
		{
			Success = true,
			Data = tutorCourses
		};
	}
}