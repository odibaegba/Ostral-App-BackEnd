using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;

namespace Ostral.Infrastructure.Repository;

public class TutorRepository : ITutorRepository
{
    private readonly OstralDBContext _context;
    private readonly IMapper _mapper;

    public TutorRepository(OstralDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatorResponseDTO<IEnumerable<TutorDTO>>> GetTutors(int pageSize = 10, int pageNumber = 1)
    {
        var tutors = _context.Tutors
            .Include(t => t.User)
            .Select(t => new TutorDTO
            {
                Id = t.Id,
                FullName = $"{t.User.FirstName} {t.User.LastName}",
                ImageUrl = t.User.ImageUrl,
                Profession = t.Profession,
            });

        return await tutors.PaginationAsync<TutorDTO, TutorDTO>(pageSize, pageNumber, _mapper);
    }
    
    public async Task<IEnumerable<TutorDTO>> GetPopularTutors()
    {
        var tutors = await _context.Tutors
            .Include(t => t.User)
            .Select(t => new TutorDTO
            {
                Id = t.Id,
                FullName = $"{t.User.FirstName} {t.User.LastName}",
                ImageUrl = t.User.ImageUrl,
                Profession = t.Profession,
            })
            .Take(6)
            .ToListAsync();

        return tutors;
    }
}