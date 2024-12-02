using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class HiringEvent
{
    public int Id { get; set; }

    public int? VacancyId { get; set; }

    public int? CandidateId { get; set; }

    public int? RecruiterId { get; set; }

    public string? Feedback { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual ICollection<HiringEventStage> HiringEventStages { get; set; } = new List<HiringEventStage>();

    public virtual Recruiter? Recruiter { get; set; }

    public virtual JobVacancy? Vacancy { get; set; }
}
