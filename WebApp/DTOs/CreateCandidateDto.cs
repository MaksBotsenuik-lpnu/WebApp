using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class CreateCandidateDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(45, ErrorMessage = "First Name cannot exceed 45 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(45, ErrorMessage = "Last Name cannot exceed 45 characters.")]
        public string LastName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? ContactInfo { get; set; }

        [StringLength(1000, ErrorMessage = "Resume cannot exceed 1000 characters.")]
        public string? Resume { get; set; }

        [StringLength(1000, ErrorMessage = "Work experience cannot exceed 1000 characters.")]
        public string? WorkExperience { get; set; }

        [StringLength(500, ErrorMessage = "Education details cannot exceed 500 characters.")]
        public string? Education { get; set; }

        [Required(ErrorMessage = "Current status is required.")]
        public string CurrentStatus { get; set; } = null!;

        public List<SkillDto> Skills { get; set; } = new();
    }

    public class SkillDto
    {
        [Required(ErrorMessage = "Skill name is required.")]
        public string Name { get; set; } = null!;
    }
}