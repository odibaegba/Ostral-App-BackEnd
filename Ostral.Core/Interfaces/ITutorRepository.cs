using Ostral.Core.DTOs;

namespace Ostral.Core.Interfaces;


public interface ITutorRepository
{
    Task<PaginatorResponseDTO<IEnumerable<TutorDTO>>> GetTutors(int pageSize = 10, int pageNumber = 1);
    Task<IEnumerable<TutorDTO>> GetPopularTutors();
}