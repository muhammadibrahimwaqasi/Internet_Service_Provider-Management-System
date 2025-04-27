using System;
using System.Collections.Generic;

namespace ISPMs.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? PartnerId { get; set; }

    public string? InvoiceDate { get; set; }

    public string InvoiceStatus { get; set; } = null!;

    public string? InvoiceAmount { get; set; }

    public virtual Partner? Partner { get; set; }
}
