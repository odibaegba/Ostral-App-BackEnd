using Ostral.Core.Interfaces;
using Ostral.Core.Results;

namespace Ostral.Core.Implementations;

public class  TutorService : ITutorService
{
    private readonly ITutorRepository _tutorRepository;

    public TutorService(ITutorRepository tutorRepository)
    {
        _tutorRepository = tutorRepository;
    }

    public async Task<TutorResult> GetTutors(int pageSize, int pageNumber)
    {
        var pagedTutors = await _tutorRepository.GetTutors(pageSize, pageNumber);

        return new TutorResult
        {
            Success = true,
            Data = pagedTutors
        };
    }

    public async Task<PopularTutorsResult> GetPopularTutors()
    {
        return new PopularTutorsResult
        {
            Success = true,
            Data = await _tutorRepository.GetPopularTutors()
        };
    }
}