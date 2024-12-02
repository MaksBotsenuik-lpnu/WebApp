using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.DTOs;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterRepository _recruiterRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public RecruiterController(IRecruiterRepository recruiterRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _recruiterRepository = recruiterRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // GET: api/recruiter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecruiterDto>>> GetAllRecruiters()
        {
            var recruiters = await _recruiterRepository.GetAllAsync();
            var recruiterDtos = _mapper.Map<IEnumerable<RecruiterDto>>(recruiters);
            return Ok(recruiterDtos);
        }

        // GET: api/recruiter/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecruiterDto>> GetRecruiterById(int id)
        {
            var recruiter = await _recruiterRepository.GetByIdAsync(id);
            if (recruiter == null)
            {
                return NotFound();
            }

            var recruiterDto = _mapper.Map<RecruiterDto>(recruiter);
            recruiterDto.JobVacancies = recruiter.JobVacancies.Select(jv => new JobVacancyDto
            {
                Id = jv.Id,
                JobTitle = jv.JobTitle,
                JobDescription = jv.JobDescription,
                VacancyStatus = jv.VacancyStatus,
                Salary = jv.Salary,
                Currency = jv.Currency,
                Location = jv.Location
            }).ToList();

            return Ok(recruiterDto);
        }

        // POST: api/recruiter
        [HttpPost]
        public async Task<ActionResult<RecruiterDto>> AddRecruiter([FromBody] CreateRecruiterDto createRecruiterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recruiter = _mapper.Map<Recruiter>(createRecruiterDto);

            if (!string.IsNullOrEmpty(createRecruiterDto.CompanyName))
            {
                var company = await _companyRepository.FindByNameAsync(createRecruiterDto.CompanyName);
                if (company == null)
                {
                    company = new Company { Name = createRecruiterDto.CompanyName };
                    await _companyRepository.AddAsync(company);
                }
                recruiter.Company = company;
            }

            try
            {
                await _recruiterRepository.AddAsync(recruiter);
                await _recruiterRepository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var recruiterDto = _mapper.Map<RecruiterDto>(recruiter);
            return CreatedAtAction(nameof(GetRecruiterById), new { id = recruiterDto.Id }, recruiterDto);
        }

        // PUT: api/recruiter/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecruiter(int id, [FromBody] UpdateRecruiterDto updateRecruiterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recruiter = await _recruiterRepository.GetByIdAsync(id);
            if (recruiter == null)
            {
                return NotFound("Recruiter not found.");
            }

            // Оновлення рекрутера тільки за наявністю нових даних
            if (!string.IsNullOrEmpty(updateRecruiterDto.FirstName))
            {
                recruiter.FirstName = updateRecruiterDto.FirstName;
            }

            if (!string.IsNullOrEmpty(updateRecruiterDto.LastName))
            {
                recruiter.LastName = updateRecruiterDto.LastName;
            }

            if (!string.IsNullOrEmpty(updateRecruiterDto.ContactInfo))
            {
                recruiter.ContactInfo = updateRecruiterDto.ContactInfo;
            }

            if (!string.IsNullOrEmpty(updateRecruiterDto.Specialization))
            {
                recruiter.Specialization = updateRecruiterDto.Specialization;
            }

            if (!string.IsNullOrEmpty(updateRecruiterDto.CompanyName))
            {
                var company = await _companyRepository.FindByNameAsync(updateRecruiterDto.CompanyName);
                if (company == null)
                {
                    company = new Company { Name = updateRecruiterDto.CompanyName };
                    await _companyRepository.AddAsync(company);
                }
                recruiter.Company = company;
            }

            try
            {
                await _recruiterRepository.UpdateAsync(recruiter);
                await _recruiterRepository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok(new { message = "Successfully updated" });
        }

        // DELETE: api/recruiter/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecruiter(int id)
        {
            try
            {
                await _recruiterRepository.DeleteAsync(id);
                await _recruiterRepository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
