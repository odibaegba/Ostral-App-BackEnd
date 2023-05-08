using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
	public interface IStudentRepository
	{
		Task<Student> GetStudentById(string id);

		Task<bool> AddStudent(Student student);
	}
}
