using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class CreateJobVacancyDto
    {
        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(255, ErrorMessage = "Job title cannot be longer than 255 characters.")]
        public string JobTitle { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Job description cannot be longer than 1000 characters.")]
        public string? JobDescription { get; set; }

        [Required(ErrorMessage = "Vacancy status is required.")]
        [CustomValidation(typeof(CreateJobVacancyDto), nameof(ValidateVacancyStatus))]
        public string VacancyStatus { get; set; } = null!; // Залишаємо тип string

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal? Salary { get; set; }

        [StringLength(3, ErrorMessage = "Currency should be a 3-letter code.")]
        public string? Currency { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "At least one skill is required.")]
        public List<SkillDto> RequiredSkills { get; set; } = new();

        public static ValidationResult? ValidateVacancyStatus(string vacancyStatus, ValidationContext context)
        {
            var allowedStatuses = new[] { "Open", "Closed", "Pending" };
            if (!allowedStatuses.Contains(vacancyStatus))
            {
                return new ValidationResult("Vacancy status must be one of the following: Open, Closed, Pending.");
            }
            return ValidationResult.Success;
        }
    }
}