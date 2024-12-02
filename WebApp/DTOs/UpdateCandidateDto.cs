using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class UpdateCandidateDto
    {
        [StringLength(45, ErrorMessage = "First Name cannot exceed 45 characters.")]
        public string? FirstName { get; set; }

        [StringLength(45, ErrorMessage = "Last Name cannot exceed 45 characters.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? ContactInfo { get; set; }

        [StringLength(1000, ErrorMessage = "Resume cannot exceed 1000 characters.")]
        public string? Resume { get; set; }

        [StringLength(1000, ErrorMessage = "Work experience cannot exceed 1000 characters.")]
        public string? WorkExperience { get; set; }

        [StringLength(500, ErrorMessage = "Education details cannot exceed 500 characters.")]
        public string? Education { get; set; }

        public string? CurrentStatus { get; set; }

        public List<SkillDto> Skills { get; set; } = new();
    }
}