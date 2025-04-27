using System;
using System.Collections.Generic;

namespace ISPMs.Models;

public partial class ScheduleJob
{
    public int ScheduleId { get; set; }

    public string TaskType { get; set; } = null!;

    public int? PackageId { get; set; }

    public int? CustomerId { get; set; }

    public string ScheduleOn { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Package? Package { get; set; }
}
