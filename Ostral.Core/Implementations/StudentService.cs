using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Core.Implementations
{
	public class StudentService : IStudentService
	{
		private readonly IStudentRepository _studentRepository;

		public StudentService(IStudentRepository studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public async Task<Result<Student>> GetStudentById(string id)
		{
			var result = await _studentRepository.GetStudentById(id);
			if (result != null) return new Result<Student> { Success = true, Data = result };
			
			return new Result<Student> { Success = false, Errors = new List<string> { "Student not found." } };
		}
	}
}
