using WebApp.Models;

namespace WebApp.Repositories
{
    public interface IRecruiterRepository: IRepository<Recruiter>
    {
        Task<IEnumerable<Recruiter>> GetRecruitersWithActiveVaca();
    }
}
