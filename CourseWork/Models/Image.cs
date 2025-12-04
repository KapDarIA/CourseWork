using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public int RoomId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
