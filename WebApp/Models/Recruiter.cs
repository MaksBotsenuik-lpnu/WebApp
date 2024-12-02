using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Recruiter
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? Specialization { get; set; }

    public int SuccessfulClosures { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<HiringEvent> HiringEvents { get; set; } = new List<HiringEvent>();

    public virtual ICollection<JobVacancy> JobVacancies { get; set; } = new List<JobVacancy>();
}
