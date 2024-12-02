using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<IEnumerable<Candidate>> GetActiveCandidatesAsync();
        Task<Skill> GetSkillByNameAsync(string skillName);
    }
}
