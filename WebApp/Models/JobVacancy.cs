using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApp.Models;

public partial class JobVacancy
{
    public int Id { get; set; }

    public string JobTitle { get; set; } = null!;

    public string? JobDescription { get; set; }

    public string? Requirements { get; set; }

    public decimal? Salary { get; set; }

    public string? Currency { get; set; }

    public string? Location { get; set; }

    public int? RecruiterId { get; set; }

    public string VacancyStatus { get; set; } = null!;

    [JsonIgnore] 
    public virtual ICollection<HiringEvent> HiringEvents { get; set; } = new List<HiringEvent>();

    [JsonIgnore]
    public virtual Recruiter? Recruiter { get; set; }
    
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
