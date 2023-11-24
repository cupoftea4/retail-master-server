using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class EventReminder
{
    public int ReminderId { get; set; }

    public DateOnly StartDate { get; set; }

    public string Description { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
