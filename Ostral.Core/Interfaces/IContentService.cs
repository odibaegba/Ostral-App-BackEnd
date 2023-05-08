﻿using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
	public interface IContentService
	{
		Task<Result<IEnumerable<ContentDTO>>> GetAllCourseContent(string courseId);
		Task<Result<ContentDetailedDTO>> GetCourseContent(string contentId);
	}
}
