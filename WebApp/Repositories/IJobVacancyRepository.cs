using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories
{
    public interface IJobVacancyRepository : IRepository<JobVacancy>
    {
        Task<IEnumerable<JobVacancy>> GetActiveJobVacanciesAsync();
        public Task<Skill?> GetSkillByNameAsync(string skillName);
        public Task AddJobVacancyWithSkillsAsync(JobVacancy jobVacancy);
    }
}