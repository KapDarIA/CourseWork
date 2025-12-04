using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class BedType
{
    public int BedTypeId { get; set; }

    public string NameBed { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
