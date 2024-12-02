using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobVacancyController : ControllerBase
    {
        private readonly IJobVacancyRepository _jobVacancyRepository;
        private readonly RecruitmentAgencyContext _context;

        public JobVacancyController(IJobVacancyRepository jobVacancyRepository, RecruitmentAgencyContext context)
        {
            _jobVacancyRepository = jobVacancyRepository;
            _context = context;
        }

        // Get all job vacancies with pagination
        [HttpGet]
        public async Task<ActionResult<PagedResult<JobVacancyDto>>> GetAllJobVacancies(int offset = 1, int limit = 20)
        {
            // Fetch total vacancies count before applying pagination
            var totalVacancies = await _context.JobVacancies.CountAsync();

            // Fetch only the required page of vacancies with limit and offset
            var paginatedVacancies = await _context.JobVacancies
                .Include(v => v.Skills) // Include related data like Skills
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync();

            // Map to DTO
            var vacancyDtos = paginatedVacancies.Select(v => new JobVacancyDto
            {
                Id = v.Id,
                JobTitle = v.JobTitle,
                JobDescription = v.JobDescription,
                VacancyStatus = v.VacancyStatus,
                Salary = v.Salary,
                Currency = v.Currency,
                Location = v.Location,
                Skills = v.Skills.Select(s => s.Name).ToList()
            }).ToList();

            // Prepare the result
            var result = new PagedResult<JobVacancyDto>
            {
                Items = vacancyDtos,
                TotalCount = totalVacancies,
                CurrentPage = offset,
                TotalPages = (int)Math.Ceiling((double)totalVacancies / limit)
            };

            return Ok(result);
        }

        // Get a single job vacancy by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<JobVacancyDto>> GetJobVacancyById(int id)
        {
            var vacancy = await _jobVacancyRepository.GetByIdAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            var vacancyDto = new JobVacancyDto
            {
                Id = vacancy.Id,
                JobTitle = vacancy.JobTitle,
                JobDescription = vacancy.JobDescription,
                VacancyStatus = vacancy.VacancyStatus,
                Salary = vacancy.Salary,
                Currency = vacancy.Currency,
                Location = vacancy.Location,
                Skills = vacancy.Skills.Select(s => s.Name).ToList()
            };

            return Ok(vacancyDto);
        }

        // Create a new job vacancy
        [HttpPost]
        public async Task<IActionResult> CreateJobVacancy([FromBody] CreateJobVacancyDto jobVacancyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobVacancy = new JobVacancy
            {
                JobTitle = jobVacancyDto.JobTitle,
                JobDescription = jobVacancyDto.JobDescription,
                VacancyStatus = jobVacancyDto.VacancyStatus,
                Salary = jobVacancyDto.Salary,
                Currency = jobVacancyDto.Currency,
                Location = jobVacancyDto.Location,
                Skills = new List<Skill>() // Initialize skills here
            };

            foreach (var skillDto in jobVacancyDto.RequiredSkills)
            {
                var existingSkill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Name == skillDto.Name);

                if (existingSkill != null)
                {
                    jobVacancy.Skills.Add(existingSkill);
                }
                else
                {
                    jobVacancy.Skills.Add(new Skill { Name = skillDto.Name });
                }
            }

            await _jobVacancyRepository.AddJobVacancyWithSkillsAsync(jobVacancy);
            await _jobVacancyRepository.SaveChangesAsync(); // Save changes after adding

            var createdVacancyDto = new JobVacancyDto
            {
                Id = jobVacancy.Id,
                JobTitle = jobVacancy.JobTitle,
                JobDescription = jobVacancy.JobDescription,
                VacancyStatus = jobVacancy.VacancyStatus,
                Salary = jobVacancy.Salary,
                Currency = jobVacancy.Currency,
                Location = jobVacancy.Location,
                Skills = jobVacancy.Skills.Select(s => s.Name).ToList()
            };

            return CreatedAtAction(nameof(GetJobVacancyById), new { id = jobVacancy.Id }, createdVacancyDto);
        }

        // Update a job vacancy
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobVacancy(int id, [FromBody] UpdateJobVacancyDto jobVacancyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vacancy = await _jobVacancyRepository.GetByIdAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            vacancy.JobTitle = jobVacancyDto.JobTitle ?? vacancy.JobTitle;
            vacancy.JobDescription = jobVacancyDto.JobDescription ?? vacancy.JobDescription;
            vacancy.VacancyStatus = jobVacancyDto.VacancyStatus ?? vacancy.VacancyStatus;
            vacancy.Salary = jobVacancyDto.Salary ?? vacancy.Salary;
            vacancy.Currency = jobVacancyDto.Currency ?? vacancy.Currency;
            vacancy.Location = jobVacancyDto.Location ?? vacancy.Location;

            // Clear existing skills and add new ones
            vacancy.Skills.Clear();
            if (jobVacancyDto.RequiredSkills != null && jobVacancyDto.RequiredSkills.Any())
            {
                foreach (var skillDto in jobVacancyDto.RequiredSkills)
                {
                    var existingSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Name == skillDto.Name);

                    if (existingSkill != null)
                    {
                        vacancy.Skills.Add(existingSkill);
                    }
                    else
                    {
                        vacancy.Skills.Add(new Skill { Name = skillDto.Name });
                    }
                }
            }

            try
            {
                await _jobVacancyRepository.UpdateAsync(vacancy);
                await _jobVacancyRepository.SaveChangesAsync(); // Ensure changes are saved
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the vacancy: " + ex.Message);
            }

            var updatedVacancyDto = new JobVacancyDto
            {
                Id = vacancy.Id,
                JobTitle = vacancy.JobTitle,
                JobDescription = vacancy.JobDescription,
                VacancyStatus = vacancy.VacancyStatus,
                Salary = vacancy.Salary,
                Currency = vacancy.Currency,
                Location = vacancy.Location,
                Skills = vacancy.Skills.Select(s => s.Name).ToList()
            };

            return Ok(updatedVacancyDto);
        }

        // Delete a job vacancy
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobVacancy(int id)
        {
            var vacancy = await _jobVacancyRepository.GetByIdAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            await _jobVacancyRepository.DeleteAsync(vacancy.Id);
            await _jobVacancyRepository.SaveChangesAsync(); // Ensure changes are saved
            return NoContent();
        }
    }

    // Paged result class to handle pagination response
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
