using System;
using System.Collections.Generic;

namespace ISPMs.Models;

public partial class Package
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public string PackagePrice { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<ScheduleJob> ScheduleJobs { get; set; } = new List<ScheduleJob>();
}
