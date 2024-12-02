using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Employer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}
