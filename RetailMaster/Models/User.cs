using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; } = null!;

    public int? ShopId { get; set; }

    public virtual ICollection<EventReminder> EventReminders { get; } = new List<EventReminder>();

    public virtual ICollection<Receipt> Receipts { get; } = new List<Receipt>();

    public virtual Shop? Shop { get; set; }
}
