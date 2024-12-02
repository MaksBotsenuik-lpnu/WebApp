using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        private readonly RecruitmentAgencyContext _context;

        public CandidateRepository(RecruitmentAgencyContext context) : base(context) 
        {
            _context = context;
        }

        // Отримати всіх кандидатів з скілами
        public new async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _context.Candidates
                .Include(c => c.Skills) // Включення скілів
                .ToListAsync();
        }
        
        public new async Task<Candidate?> GetByIdAsync(int id)
        {
            return await _context.Candidates
                .Include(c => c.Skills) // Включення скілів
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        // Отримати всіх активних кандидатів з скілами
        public async Task<IEnumerable<Candidate>> GetActiveCandidatesAsync()
        {
            return await _context.Candidates
                .Where(c => c.CurrentStatus == "Active")
                .Include(c => c.Skills) // Включення скілів
                .ToListAsync();
        }
        
        public async Task<Skill> GetSkillByNameAsync(string skillName)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(s => s.Name == skillName);
        }

    }
}