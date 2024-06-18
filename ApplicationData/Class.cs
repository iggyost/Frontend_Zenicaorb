using System;
using System.Collections.Generic;

namespace Frontend_Zenicaorb.ApplicationData;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
