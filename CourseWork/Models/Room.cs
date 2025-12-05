using System;
using System.Collections.Generic;
using System.IO;

namespace CourseWork.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int RoomTypeId { get; set; }

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public string IsAvailable { get; set; } = null!;

    public int BedTypeId { get; set; }

    public bool FoodIncluded { get; set; }

    public bool MiniBarIncluded { get; set; }

    public bool RiverView { get; set; }

    public bool BabyBed { get; set; }

    public bool ForDisabled { get; set; }

    public string? Image { get; set; }

    public virtual BedType BedType { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomType RoomType { get; set; } = null!;

    public string PathImage()
    {
        if (string.IsNullOrWhiteSpace(Image))
            return "/Images/default.jpg";

        return $"/Images/{Image}";
    }
}
