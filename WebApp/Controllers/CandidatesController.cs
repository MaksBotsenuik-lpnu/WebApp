using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.DTOs;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidatesController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        // Get all candidates
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateRepository.GetAllAsync();

            var candidateDtos = candidates.Select(c => new GetCandidateDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                ContactInfo = c.ContactInfo,
                Resume = c.Resume,
                WorkExperience = c.WorkExperience,
                Education = c.Education,
                CurrentStatus = c.CurrentStatus,
                Skills = c.Skills.Select(s => s.Name).ToList()  // Mapping skills
            }).ToList();

            return Ok(candidateDtos);
        }

        // Get a candidate by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            var candidateDto = new GetCandidateDto
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                ContactInfo = candidate.ContactInfo,
                Resume = candidate.Resume,
                WorkExperience = candidate.WorkExperience,
                Education = candidate.Education,
                CurrentStatus = candidate.CurrentStatus,
                Skills = candidate.Skills.Select(s => s.Name).ToList() // Mapping skills
            };

            return Ok(candidateDto);
        }

        // Create a new candidate
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = new Candidate
            {
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                ContactInfo = candidateDto.ContactInfo,
                Resume = candidateDto.Resume,
                WorkExperience = candidateDto.WorkExperience,
                Education = candidateDto.Education,
                CurrentStatus = candidateDto.CurrentStatus,
                Skills = new List<Skill>() // Ініціалізуємо список навичок
            };

            foreach (var skillDto in candidateDto.Skills)
            {
                var existingSkill = await _candidateRepository.GetSkillByNameAsync(skillDto.Name);
                if (existingSkill != null)
                {
                    candidate.Skills.Add(existingSkill);
                }
                else
                {
                    var newSkill = new Skill { Name = skillDto.Name };
                    candidate.Skills.Add(newSkill);
                }
            }

            await _candidateRepository.AddAsync(candidate);
            await _candidateRepository.SaveChangesAsync();

            var createdCandidateDto = new GetCandidateDto
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                ContactInfo = candidate.ContactInfo,
                Resume = candidate.Resume,
                WorkExperience = candidate.WorkExperience,
                Education = candidate.Education,
                CurrentStatus = candidate.CurrentStatus,
                Skills = candidate.Skills.Select(s => s.Name).ToList()
            };

            return CreatedAtAction(nameof(Get), new { id = candidate.Id }, createdCandidateDto);
        }

        // Update an existing candidate
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            if (candidateDto.FirstName != null)
            {
                candidate.FirstName = candidateDto.FirstName;
            }
    
            if (candidateDto.LastName != null)
            {
                candidate.LastName = candidateDto.LastName;
            }
    
            if (candidateDto.ContactInfo != null)
            {
                candidate.ContactInfo = candidateDto.ContactInfo;
            }
    
            if (candidateDto.Resume != null)
            {
                candidate.Resume = candidateDto.Resume;
            }
    
            if (candidateDto.WorkExperience != null)
            {
                candidate.WorkExperience = candidateDto.WorkExperience;
            }
    
            if (candidateDto.Education != null)
            {
                candidate.Education = candidateDto.Education;
            }

            if (candidateDto.CurrentStatus != null)
            {
                candidate.CurrentStatus = candidateDto.CurrentStatus;
            }

            // Update skills
            if (candidateDto.Skills != null && candidateDto.Skills.Any())
            {
                candidate.Skills.Clear(); // Clear existing skills
                foreach (var skillDto in candidateDto.Skills)
                {
                    var existingSkill = await _candidateRepository.GetSkillByNameAsync(skillDto.Name);
                    if (existingSkill != null)
                    {
                        candidate.Skills.Add(existingSkill);
                    }
                    else
                    {
                        var newSkill = new Skill { Name = skillDto.Name };
                        candidate.Skills.Add(newSkill);
                    }
                }
            }

            await _candidateRepository.UpdateAsync(candidate);
            await _candidateRepository.SaveChangesAsync();

            return Ok(new { message = "Successfully updated" });
        }

        // Delete a candidate
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await _candidateRepository.DeleteAsync(id);
            await _candidateRepository.SaveChangesAsync();

            return NoContent();
        }

        // Get active candidates
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCandidates()
        {
            var activeCandidates = await _candidateRepository.GetActiveCandidatesAsync();

            var candidateDtos = activeCandidates.Select(c => new GetCandidateDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                ContactInfo = c.ContactInfo,
                Resume = c.Resume,
                WorkExperience = c.WorkExperience,
                Education = c.Education,
                CurrentStatus = c.CurrentStatus,
                Skills = c.Skills.Select(s => s.Name).ToList() // Mapping skills
            }).ToList();

            return Ok(candidateDtos);
        }
    }
}
