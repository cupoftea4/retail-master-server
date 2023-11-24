using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<PersonalDiscount> PersonalDiscounts { get; } = new List<PersonalDiscount>();

    public virtual ICollection<Receipt> Receipts { get; } = new List<Receipt>();
}
