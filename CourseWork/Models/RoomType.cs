using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string NameType { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
