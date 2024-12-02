using WebApp.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApp.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly RecruitmentAgencyContext _context;

    public CompanyRepository(RecruitmentAgencyContext context)
    {
        _context = context;
    }

    public async Task<Company> FindByNameAsync(string name)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
