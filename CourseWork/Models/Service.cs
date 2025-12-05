using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string NameService { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}
