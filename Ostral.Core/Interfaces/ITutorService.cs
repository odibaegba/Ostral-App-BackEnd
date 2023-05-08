using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public class TutorResult: Result<PaginatorResponseDTO<IEnumerable<TutorDTO>>>{}

public class PopularTutorsResult : Result<IEnumerable<TutorDTO>>
{
}
	
public interface ITutorService
{
	public Task<TutorResult> GetTutors(int pageSize, int pageNumber);
	public Task<PopularTutorsResult> GetPopularTutors();
}