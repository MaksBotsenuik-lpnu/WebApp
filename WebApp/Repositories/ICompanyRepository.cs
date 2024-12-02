using WebApp.Models;
namespace WebApp.Repositories;

public interface ICompanyRepository
{
    Task<Company> FindByNameAsync(string name);
    Task AddAsync(Company company);
    Task SaveChangesAsync();
}
