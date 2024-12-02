using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class UpdateJobVacancyDto
    {
        [StringLength(255, ErrorMessage = "Job title cannot be longer than 255 characters.")]
        public string? JobTitle { get; set; }

        [StringLength(1000, ErrorMessage = "Job description cannot be longer than 1000 characters.")]
        public string? JobDescription { get; set; }

        [CustomValidation(typeof(UpdateJobVacancyDto), nameof(ValidateVacancyStatus))]
        public string? VacancyStatus { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal? Salary { get; set; }

        [StringLength(3, ErrorMessage = "Currency should be a 3-letter code.")]
        public string? Currency { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string? Location { get; set; }

        public List<SkillDto> RequiredSkills { get; set; } = new();

        public static ValidationResult? ValidateVacancyStatus(string? vacancyStatus, ValidationContext context)
        {
            var allowedStatuses = new[] { "Open", "Closed", "Pending" };
            if (vacancyStatus != null && !allowedStatuses.Contains(vacancyStatus))
            {
                return new ValidationResult("Vacancy status must be one of the following: Open, Closed, Pending.");
            }
            return ValidationResult.Success;
        }
    }
}