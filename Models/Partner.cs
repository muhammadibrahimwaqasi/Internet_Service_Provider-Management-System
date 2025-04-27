using System;
using System.Collections.Generic;

namespace ISPMs.Models;

public partial class Partner
{
    public int PartnerId { get; set; }

    public string PName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Cnic { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
