using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class Department
{
    public int Id { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Manager { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual Hospital HospitalCodeNavigation { get; set; } = null!;
}
