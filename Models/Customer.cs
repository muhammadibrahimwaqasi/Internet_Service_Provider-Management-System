using System;
using System.Collections.Generic;

namespace ISPMs.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public int Password { get; set; }

    public string? Address { get; set; }

    public string MobileNo { get; set; } = null!;

    public string Cnic { get; set; } = null!;

    public string CustomerStatus { get; set; } = null!;

    public string ActivationDate { get; set; } = null!;

    public string ExpiryDate { get; set; } = null!;

    public int PackageId { get; set; }

    public int PartnerId { get; set; }

    public virtual Package Package { get; set; } = null!;

    public virtual Partner Partner { get; set; } = null!;

    public virtual ICollection<ScheduleJob> ScheduleJobs { get; set; } = new List<ScheduleJob>();
}
