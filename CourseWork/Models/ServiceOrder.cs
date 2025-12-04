using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class ServiceOrder
{
    public int ServiceOrderId { get; set; }

    public int ServiceId { get; set; }

    public int UserId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
