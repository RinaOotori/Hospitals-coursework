using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class Post
{
    public int Id { get; set; }

    public string PostCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Branch { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
