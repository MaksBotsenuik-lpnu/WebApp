using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class HiringEventStage
{
    public int Id { get; set; }
    
    public int HiringEventId { get; set; }

    public string Stage { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual HiringEvent HiringEvent { get; set; } = null!;
}
