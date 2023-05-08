using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
	public interface IStudentService
	{
		Task<Result<Student>> GetStudentById(string id);
	}
}