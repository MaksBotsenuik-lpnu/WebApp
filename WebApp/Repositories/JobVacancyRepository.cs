using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class JobVacancyRepository : Repository<JobVacancy>, IJobVacancyRepository
    {
        private readonly RecruitmentAgencyContext _context;

        public JobVacancyRepository(RecruitmentAgencyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Skill?> GetSkillByNameAsync(string skillName)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(s => s.Name == skillName);
        }

        public async Task AddJobVacancyWithSkillsAsync(JobVacancy jobVacancy)
        {
            foreach (var skill in jobVacancy.Skills)
            {
                var existingSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Name == skill.Name);

                if (existingSkill != null)
                {
                    skill.Id = existingSkill.Id;
                    skill.Name = existingSkill.Name;
                }
                else
                {
                    _context.Skills.Add(skill);
                }
            }

            _context.JobVacancies.Add(jobVacancy);
            await _context.SaveChangesAsync();
        }

        public new async Task<IEnumerable<JobVacancy>> GetAllAsync()
        {
            return await _context.JobVacancies
                .Include(jv => jv.Skills)
                .ToListAsync();
        }

        public new async Task<JobVacancy?> GetByIdAsync(int id)
        {
            return await _context.JobVacancies
                .Include(jv => jv.Skills)
                .FirstOrDefaultAsync(jv => jv.Id == id);
        }

        public async Task<IEnumerable<JobVacancy>> GetActiveJobVacanciesAsync()
        {
            return await _context.JobVacancies
                .Where(jv => jv.VacancyStatus == "Active")
                .Include(jv => jv.Skills)
                .ToListAsync();
        }
    }
}
