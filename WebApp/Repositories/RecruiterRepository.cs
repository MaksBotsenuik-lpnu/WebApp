using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class RecruiterRepository : Repository<Recruiter>, IRecruiterRepository
    {
        private readonly RecruitmentAgencyContext _context;
        
        public RecruiterRepository(RecruitmentAgencyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recruiter>> GetRecruitersWithActiveVaca()
        {
            return await _context.Recruiters
                .Where(r => r.JobVacancies.Any(v => v.VacancyStatus=="Active"))
                .ToListAsync();
        }
        
        public new async Task<IEnumerable<Recruiter>> GetAllAsync()
        {
            return await _context.Recruiters
                .Include(r => r.Company)
                .Include(r => r.JobVacancies)
                .ToListAsync();
        }
        
        public new async Task<Recruiter> GetByIdAsync(int id)
        {
            return await _context.Recruiters
                .Include(r => r.JobVacancies)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}